using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class VocalLane : MonoBehaviour
{
    public MicCursor m_Cursor;
    public GameObject m_NotePrefab;

    private struct Line
    {
        public double time;
        public double deltaTime;

        public Vector3 startPos;
        public Vector3 endPos;

        public int startNoteNum;
        public int endNoteNum;

        public string lyric;

        public bool isOverDrive;
    }

    private List<Line> m_Lines;
    private List<Track.Note> m_Notes;
    private bool m_LinesGenerated;

    private int m_SpawnIndex;
    private int m_InputIndex;

    private VocalTrack m_Track;

    private float m_ScoreTime;
    private float m_FullScoreTime;

    // Start is called before the first frame update
    void Start()
    {
        m_Notes = new List<Track.Note>();
        m_LinesGenerated = false;

        m_SpawnIndex = 0;
        m_InputIndex = 0;

        m_Track = transform.parent.GetComponent<VocalTrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LinesGenerated)
            return;

        // SPAWN NOTES
        if (m_SpawnIndex < m_Lines.Count)
        {
            Line currLine = m_Lines[m_SpawnIndex];
            double currNoteSpawnTime = currLine.time - m_Track.m_NoteRollTime;
            if (m_Track.GetTimeElapsed() >= currNoteSpawnTime)
            {
                GameObject noteObject = Instantiate(m_NotePrefab, transform);
                Vector3 startPos = currLine.startPos;
                Vector3 endPos = currLine.endPos;
                noteObject.GetComponent<LineRenderer>().SetPosition(0, startPos);
                noteObject.GetComponent<LineRenderer>().SetPosition(1, endPos);

                string lyric = currLine.lyric;
                noteObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = lyric;

                noteObject.AddComponent<VocalNote>();
                noteObject.GetComponent<VocalNote>().SetDeltaTime(currLine.deltaTime);
                noteObject.GetComponent<VocalNote>().SetOverDrive(currLine.isOverDrive);

                noteObject.GetComponent<VocalNote>().Spawn(m_Track.GetTimeElapsed());

                m_SpawnIndex++;
            }
        }

        // USER INTERACTION
        if (m_InputIndex < m_Lines.Count)
        {
            double noteStartTime = m_Lines[m_InputIndex].time;
            double deltaTime = m_Lines[m_InputIndex].deltaTime;
            double noteEndTime = noteStartTime + deltaTime;
            double timeElapsed = m_Track.GetTimeElapsed();
            timeElapsed -= m_Track.m_NotesDelay / 1000.0f;

            if (timeElapsed >= noteStartTime)
            {
                int startPitch = m_Lines[m_InputIndex].startNoteNum;
                int endPitch = m_Lines[m_InputIndex].endNoteNum;
                int pitchDiff = Math.Abs(startPitch - endPitch);

                double alpha = (timeElapsed - noteStartTime) / deltaTime;
                int currNotePitch = (int)(startPitch + pitchDiff * alpha);
                int correction = m_Track.m_CorrectionError;
                int minCorrection = currNotePitch - correction;
                int maxCorrection = currNotePitch + correction;
                int cursorPitch = m_Cursor.CurrentPitch();

                float currTime = Time.time;
                if (currTime - m_FullScoreTime > 0.1f)
                {
                    m_Track.m_FullScore++;
                    m_FullScoreTime = currTime;
                }

                if (cursorPitch > minCorrection && cursorPitch < maxCorrection)
                {
                    if (currTime - m_ScoreTime > 0.1f)
                    {
                        m_Track.m_CurrentScore++;
                        m_ScoreTime = currTime;
                    }
                }
            }
            if (timeElapsed >= noteEndTime)
                m_InputIndex++;
        }
    }

    public void AddNote(Track.Note note)
    {
        m_Notes.Add(note);
    }

    public void GenerateLines()
    {
        m_Lines = new List<Line>();

        float speed = m_Track.m_NoteRollDistance / m_Track.m_NoteRollTime;
        int index = 0;
        foreach (var note in m_Notes)
        {
            Line line = new Line();
            line.time = note.hitTime;
            line.deltaTime = note.deltaTime;
            float yPos = (note.noteNumber - 36) / 48.0f - 0.5f;
            float length = speed * (float)note.deltaTime;
            line.startPos = new Vector3(0, 0, yPos);
            line.endPos = new Vector3(length, 0, yPos);
            line.startNoteNum = note.noteNumber;
            line.endNoteNum = note.noteNumber;
            line.isOverDrive = note.isOverDrive;

            if (note.lyric == "+")
            {
                if (m_Lines.Count > 1)
                {
                    Line connector = new Line();
                    connector.startPos = m_Lines[m_Lines.Count - 1].endPos;
                    connector.startPos.x = 0;
                    connector.endPos = line.startPos;
                    double deltaTime = note.hitTime - m_Notes[index - 1].hitTime;
                    deltaTime -= m_Notes[index - 1].deltaTime;
                    connector.time = line.time - deltaTime;
                    connector.deltaTime = deltaTime;
                    connector.endPos.x = speed * (float)deltaTime;
                    connector.startNoteNum = m_Notes[index - 1].noteNumber;
                    connector.endNoteNum = note.noteNumber;
                    connector.lyric = "";
                    connector.isOverDrive = line.isOverDrive;
                    m_Lines.Add(connector);
                }

                line.lyric = "";
            }
            else
                line.lyric = note.lyric;

            m_Lines.Add(line);

            index++;
        }

        m_LinesGenerated = true;
    }
}
