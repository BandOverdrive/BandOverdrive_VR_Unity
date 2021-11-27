using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class VocalTrack : Track
{
    public VocalLane m_VocalLane;

    public enum VocalTrackType
    {
        Vocal,
        Harmony1,
        Harmony2,
        Harmony3
    }
    public VocalTrackType m_VocalTrackType = VocalTrackType.Vocal;
    public int m_CorrectionError = 3;

    private List<string> m_Lyrics;

    public int m_FullScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Lyrics = new List<string>();

        Invoke(nameof(LoadAndPlay), 0.1f);
    }

    override protected void TrackUpdate()
    {
        if (m_FullScore != 0.0f)
            m_Accuracy = (float)m_CurrentScore / m_FullScore;
        else
            m_Accuracy = 0.0f;
    }

    private void LoadAndPlay()
    {
        LoadSong();

        switch (m_CurrentLevel)
        {
            case Level.Easy:
                m_CorrectionError = 7;
                break;
            case Level.Medium:
                m_CorrectionError = 5;
                break;
            case Level.Hard:
                m_CorrectionError = 3;
                break;
            case Level.Expert:
                m_CorrectionError = 1;
                break;
        }

        string trackNameString = "";
        switch (m_VocalTrackType)
        {
            case VocalTrackType.Vocal:
                trackNameString = "Sequence/Track Name (PART VOCALS)";
                break;
            case VocalTrackType.Harmony1:
                trackNameString = "Sequence/Track Name (HARM1)";
                break;
            case VocalTrackType.Harmony2:
                trackNameString = "Sequence/Track Name (HARM2)";
                break;
            case VocalTrackType.Harmony3:
                trackNameString = "Sequence/Track Name (HARM3)";
                break;
        }
        var chunks = m_MidiFile.GetTrackChunks();
        foreach (var chunk in chunks)
        {
            if (chunk.Events[0].ToString() == trackNameString)
            {
                // GET LYRICS
                var events = chunk.GetTimedEvents();
                TimedEvent[] evnts = new TimedEvent[events.Count];
                events.CopyTo(evnts, 0);
                foreach (var evnt in evnts)
                {
                    if (evnt.Event.EventType == MidiEventType.Lyric)
                    {
                        string lyric = evnt.Event.ToString();
                        lyric = lyric.Substring(7, lyric.Length - 8);
                        m_Lyrics.Add(lyric);
                    }
                }

                // GET NOTES
                var notes = chunk.GetNotes();
                m_Notes = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
                notes.CopyTo(m_Notes, 0);
                int lyricIndex = 0;
                foreach (var note in m_Notes)
                {
                    if (note.NoteNumber == 116)
                    {
                        m_ODSection = true;
                        m_CurODEndTime = note.GetTimedNoteOffEvent().Time;
                    }

                    if (note.NoteNumber >= 36 && note.NoteNumber <= 84)
                    {
                        m_VocalLane.AddNote(CreateNote(note, lyricIndex));
                        lyricIndex++;
                    }
                }
                m_VocalLane.GenerateLines();

                break;
            }
        }
        
        PlaySong();
    }

    private Note CreateNote(Melanchall.DryWetMidi.Interaction.Note note, int lyricIndex)
    {
        Note noteNew = new Note();
        TempoMap tempoMap = m_MidiFile.GetTempoMap();

        var metricTimeSpan =
            TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
        double hitTime = (double)metricTimeSpan.Minutes * 60.0f;
        hitTime += metricTimeSpan.Seconds;
        hitTime += (double)metricTimeSpan.Milliseconds / 1000.0f + m_NotesDelay;
        noteNew.hasTail = true;
        noteNew.hitTime = hitTime;

        var deltaTimeSpan =
                TimeConverter.ConvertTo<MetricTimeSpan>(note.Length, tempoMap);
        double deltaTime = (double)deltaTimeSpan.Minutes * 60.0f;
        deltaTime += deltaTimeSpan.Seconds;
        deltaTime += (double)deltaTimeSpan.Milliseconds / 1000.0f;
        noteNew.deltaTime = deltaTime;

        noteNew.isSolo = false;
        noteNew.isOverDrive = false;
        noteNew.isHopo = false;
        noteNew.isTom = false;

        noteNew.noteNumber = note.NoteNumber;
        if (lyricIndex >= m_Lyrics.Count)
            noteNew.lyric = "";
        else
            noteNew.lyric = m_Lyrics[lyricIndex];

        if (m_ODSection)
        {
            if (note.Time < m_CurODEndTime)
                noteNew.isOverDrive = true;
            else
                m_ODSection = false;
        }

        return noteNew;
    }
}
