using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public HitButton m_InputButton;
    public GameObject m_NotePrefab;

    private List<Track.Note> m_Notes;
    private List<Note> m_CurrNotes;

    private int m_SpawnIndex;
    private int m_InputIndex;

    private Track m_Track;

    private float m_ScoreTime;

    // Start is called before the first frame update
    void Start()
    {
        m_Notes = new List<Track.Note>();
        m_CurrNotes = new List<Note>();

        m_SpawnIndex = 0;
        m_InputIndex = 0;

        m_Track = transform.parent.GetComponent<Track>();
    }

    // Update is called once per frame
    void Update()
    {
        // SPAWN NOTES
        if (m_SpawnIndex < m_Notes.Count)
        {
            Track.Note currNote = m_Notes[m_SpawnIndex];
            double currNoteSpawnTime = currNote.hitTime - m_Track.m_NoteRollTime;
            if (m_Track.GetTimeElapsed() >= currNoteSpawnTime)
            {
                GameObject noteObject = Instantiate(m_NotePrefab, transform);
                noteObject.AddComponent<Note>();
                if (currNote.hasTail)
                    noteObject.GetComponent<Note>().SetTail(currNote.deltaTime);

                noteObject.GetComponent<Note>().SetSolo(currNote.isSolo);
                noteObject.GetComponent<Note>().SetOverDrive(currNote.isOverDrive);
                noteObject.GetComponent<Note>().SetHopo(currNote.isHopo);
                noteObject.GetComponent<Note>().SetTom(currNote.isTom);

                noteObject.GetComponent<Note>().Spawn(m_Track.GetTimeElapsed());

                m_CurrNotes.Add(noteObject.GetComponent<Note>());
                m_SpawnIndex++;
            }
        }

        // USER INTERACTIONS
        if (m_InputIndex < m_CurrNotes.Count)
        {
            double hitTime = m_Notes[m_InputIndex].hitTime;
            double errorTime = m_Track.m_ErrorTime;
            double timeElapsed = m_Track.GetTimeElapsed();
            timeElapsed -= m_Track.m_NotesDelay / 1000.0f;

            if (!m_CurrNotes[m_InputIndex].IsHit())
            {
                if (m_InputButton.IsHit())
                {
                    if (Math.Abs(timeElapsed - hitTime) < errorTime)
                    {
                        if (m_Notes[m_InputIndex].isTom)
                        {
                            if (m_InputButton.IsTomHit())
                                HitNote();
                        }
                        else if (m_Notes[m_InputIndex].isHopo)
                        {
                            if (m_InputButton.IsHopoHit())
                                HitNote();
                        }
                        else
                        {
                            if (!m_InputButton.IsTomHit() && !m_InputButton.IsHopoHit())
                                HitNote();
                        }
                    }
                    else
                    {
                        // TOO EARLY
                        // This is a bug to be fixed
                        // The hit time is 0.1s and this branch will be
                        // run in every frame during 0.1s
                        // so the HP will be subtracted multiple times
                        m_Track.m_CurrentHP -= 1;
                    }
                }
                if (hitTime + errorTime <= timeElapsed)
                {
                    // TOO LATE
                    m_Track.m_CurrentHP -= 1;
                    m_Track.m_CurrentCombo = 0;
                    m_InputIndex++;
                }
            }
            else if (m_Notes[m_InputIndex].hasTail)
            {
                if (m_InputButton.IsPressed())
                {
                    // PRESSING
                    double pressingElapsedTime = Math.Abs(timeElapsed - hitTime);
                    double deltaTime = m_Notes[m_InputIndex].deltaTime;
                    deltaTime -= pressingElapsedTime + m_Track.m_NotesDelay;
                    deltaTime -= m_Track.m_ErrorTime;
                    m_CurrNotes[m_InputIndex].UpdateTail(deltaTime);
                    if (deltaTime <= 0.0f)
                        DestroyNote();

                    float currTime = Time.time;
                    if (currTime - m_ScoreTime > 0.1f)
                    {
                        m_Track.m_CurrentScore++;
                        m_ScoreTime = currTime;
                    }
                }
                else
                {
                    m_CurrNotes[m_InputIndex].StopPressing();
                    m_InputIndex++;
                }
            }
            // There is no chance for any note to go into this branch
            // Just wrote this in case
            else
                DestroyNote();
        }
    }

    private void DestroyNote()
    {
        Destroy(m_CurrNotes[m_InputIndex].gameObject);
        m_InputIndex++;
    }

    private void HitNote()
    {
        m_CurrNotes[m_InputIndex].Hit();
        if (!m_Notes[m_InputIndex].hasTail)
            DestroyNote();

        m_Track.m_CurrentScore += 10;
        m_Track.m_CurrentHP += 3;
        m_Track.m_CurrentCombo++;
        m_Track.m_HitTotal++;
    }

    public void AddNote(Track.Note note)
    {
        m_Notes.Add(note);
    }
}
