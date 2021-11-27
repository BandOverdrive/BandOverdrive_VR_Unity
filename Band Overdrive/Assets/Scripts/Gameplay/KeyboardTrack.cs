using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class KeyboardTrack : Track
{
    public Lane m_LaneC3;
    public Lane m_LaneCS3;
    public Lane m_LaneD3;
    public Lane m_LaneDS3;
    public Lane m_LaneE3;
    public Lane m_LaneF3;
    public Lane m_LaneFS3;
    public Lane m_LaneG3;
    public Lane m_LaneGS3;
    public Lane m_LaneA3;
    public Lane m_LaneAS3;
    public Lane m_LaneB3;
    public Lane m_LaneC4;
    public Lane m_LaneCS4;
    public Lane m_LaneD4;
    public Lane m_LaneDS4;
    public Lane m_LaneE4;
    public Lane m_LaneF4;
    public Lane m_LaneFS4;
    public Lane m_LaneG4;
    public Lane m_LaneGS4;
    public Lane m_LaneA4;
    public Lane m_LaneAS4;
    public Lane m_LaneB4;
    public Lane m_LaneC5;

    private int m_NotesCount;

    // Start is called before the first frame update
    void Start()
    {
        m_NotesCount = 0;

        Invoke(nameof(LoadAndPlay), 0.1f);
    }

    protected override void TrackUpdate()
    {
        if (m_NotesCount != 0)
            m_Accuracy = (float)m_HitTotal / m_NotesCount;
        else
            m_Accuracy = 0.0f;
    }

    private void LoadAndPlay()
    {
        LoadSong();

        string trackNameString = "";
        switch (m_CurrentLevel)
        {
            case Level.Easy:
                trackNameString = "Sequence/Track Name (PART REAL_KEYS_E)";
                m_NoteRollTime = 6;
                break;
            case Level.Medium:
                trackNameString = "Sequence/Track Name (PART REAL_KEYS_M)";
                m_NoteRollTime = 5;
                break;
            case Level.Hard:
                trackNameString = "Sequence/Track Name (PART REAL_KEYS_H)";
                m_NoteRollTime = 4;
                break;
            case Level.Expert:
                trackNameString = "Sequence/Track Name (PART REAL_KEYS_X)";
                m_NoteRollTime = 3;
                break;
        }

        var chunks = m_MidiFile.GetTrackChunks();
        foreach (var chunk in chunks)
        {
            if (chunk.Events[0].ToString() == trackNameString)
            {
                var notes = chunk.GetNotes();
                m_Notes = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
                notes.CopyTo(m_Notes, 0);

                foreach (var note in m_Notes)
                {
                    if (note.NoteNumber == 115)
                    {
                        m_SoloSection = true;
                        m_CurSoloEndTime = note.GetTimedNoteOffEvent().Time;
                    }
                    if (note.NoteNumber == 116)
                    {
                        m_ODSection = true;
                        m_CurODEndTime = note.GetTimedNoteOffEvent().Time;
                    }

                    if (note.NoteNumber >= 48 && note.NoteNumber <= 72)
                    {
                        Note noteNew = CreateNote(note);
                        if (note.NoteNumber == 48)
                            m_LaneC3.AddNote(noteNew);
                        if (note.NoteNumber == 49)
                            m_LaneCS3.AddNote(noteNew);
                        if (note.NoteNumber == 50)
                            m_LaneD3.AddNote(noteNew);
                        if (note.NoteNumber == 51)
                            m_LaneDS3.AddNote(noteNew);
                        if (note.NoteNumber == 52)
                            m_LaneE3.AddNote(noteNew);
                        if (note.NoteNumber == 53)
                            m_LaneF3.AddNote(noteNew);
                        if (note.NoteNumber == 54)
                            m_LaneFS3.AddNote(noteNew);
                        if (note.NoteNumber == 55)
                            m_LaneG3.AddNote(noteNew);
                        if (note.NoteNumber == 56)
                            m_LaneGS3.AddNote(noteNew);
                        if (note.NoteNumber == 57)
                            m_LaneA3.AddNote(noteNew);
                        if (note.NoteNumber == 58)
                            m_LaneAS3.AddNote(noteNew);
                        if (note.NoteNumber == 59)
                            m_LaneB3.AddNote(noteNew);
                        if (note.NoteNumber == 60)
                            m_LaneC4.AddNote(noteNew);
                        if (note.NoteNumber == 61)
                            m_LaneCS4.AddNote(noteNew);
                        if (note.NoteNumber == 62)
                            m_LaneD4.AddNote(noteNew);
                        if (note.NoteNumber == 63)
                            m_LaneDS4.AddNote(noteNew);
                        if (note.NoteNumber == 64)
                            m_LaneE4.AddNote(noteNew);
                        if (note.NoteNumber == 65)
                            m_LaneF4.AddNote(noteNew);
                        if (note.NoteNumber == 66)
                            m_LaneFS4.AddNote(noteNew);
                        if (note.NoteNumber == 67)
                            m_LaneG4.AddNote(noteNew);
                        if (note.NoteNumber == 68)
                            m_LaneGS4.AddNote(noteNew);
                        if (note.NoteNumber == 69)
                            m_LaneA4.AddNote(noteNew);
                        if (note.NoteNumber == 70)
                            m_LaneAS4.AddNote(noteNew);
                        if (note.NoteNumber == 71)
                            m_LaneB4.AddNote(noteNew);
                        if (note.NoteNumber == 72)
                            m_LaneC5.AddNote(noteNew);
                    }
                }

                break;
            }
        }

        PlaySong();
    }

    private Note CreateNote(Melanchall.DryWetMidi.Interaction.Note note)
    {
        Note noteNew = new Note();
        TempoMap tempoMap = m_MidiFile.GetTempoMap();

        var metricTimeSpan =
            TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap);
        double hitTime = (double)metricTimeSpan.Minutes * 60.0f;
        hitTime += metricTimeSpan.Seconds;
        hitTime += (double)metricTimeSpan.Milliseconds / 1000.0f + m_NotesDelay;
        noteNew.hasTail = false;
        noteNew.hitTime = hitTime;
        noteNew.deltaTime = 0.0f;

        string fraction = note.LengthAs(TimeSpanType.Musical, tempoMap).ToString();
        int numerator = int.Parse(fraction.Split('/')[0]);
        int denominator = int.Parse(fraction.Split('/')[1]);
        float length = (float)numerator / denominator;
        if (length > (float)1 / 16)
        {
            var deltaTimeSpan =
                TimeConverter.ConvertTo<MetricTimeSpan>(note.Length, tempoMap);
            double deltaTime = (double)deltaTimeSpan.Minutes * 60.0f;
            deltaTime += deltaTimeSpan.Seconds;
            deltaTime += (double)deltaTimeSpan.Milliseconds / 1000.0f;
            noteNew.hasTail = true;
            noteNew.deltaTime = deltaTime;
        }

        noteNew.isSolo = false;
        noteNew.isOverDrive = false;
        noteNew.isHopo = false;
        noteNew.isTom = false;
        noteNew.noteNumber = 0;
        noteNew.lyric = "";

        if (m_SoloSection)
        {
            if (note.Time < m_CurSoloEndTime)
                noteNew.isSolo = true;
            else
                m_SoloSection = false;
        }
        if (m_ODSection)
        {
            if (note.Time < m_CurODEndTime)
                noteNew.isOverDrive = true;
            else
                m_ODSection = false;
        }

        m_NotesCount++;

        return noteNew;
    }
}
