using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class DrumTrack : Track
{
    public Lane m_RedLane;
    public Lane m_YellowLane;
    public Lane m_BlueLane;
    public Lane m_GreenLane;
    public Lane m_OrangeLane;

    private bool m_GreenCymbal;
    private bool m_BlueCymbal;
    private bool m_YellowCymbal;

    // Start is called before the first frame update
    void Start()
    {
        m_GreenCymbal = false;
        m_BlueCymbal = false;
        m_YellowCymbal = false;

        Invoke(nameof(LoadAndPlay), 0.1f);
    }

    private void LoadAndPlay()
    {
        LoadSong();

        int greenNoteNum;
        int blueNoteNum;
        int yellowNoteNum;
        int redNoteNum;
        int orangeNoteNum;
        if (m_CurrentLevel == Level.Expert)
        {
            greenNoteNum = 100;
            blueNoteNum = 99;
            yellowNoteNum = 98;
            redNoteNum = 97;
            orangeNoteNum = 96;

            m_NoteRollTime = 2;
        }
        else if (m_CurrentLevel == Level.Hard)
        {
            greenNoteNum = 88;
            blueNoteNum = 87;
            yellowNoteNum = 86;
            redNoteNum = 85;
            orangeNoteNum = 84;

            m_NoteRollTime = 3;
        }
        else if (m_CurrentLevel == Level.Medium)
        {
            greenNoteNum = 76;
            blueNoteNum = 75;
            yellowNoteNum = 74;
            redNoteNum = 73;
            orangeNoteNum = 72;

            m_NoteRollTime = 4;
        }
        else
        {
            greenNoteNum = 64;
            blueNoteNum = 63;
            yellowNoteNum = 62;
            redNoteNum = 61;
            orangeNoteNum = 60;

            m_NoteRollTime = 5;
        }

        string trackNameString = "Sequence/Track Name (PART DRUMS)";
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
                    if (note.NoteNumber == 103)
                    {
                        m_SoloSection = true;
                        m_CurSoloEndTime = note.GetTimedNoteOffEvent().Time;
                    }
                    if (note.NoteNumber == 116)
                    {
                        m_ODSection = true;
                        m_CurODEndTime = note.GetTimedNoteOffEvent().Time;
                    }

                    if (note.NoteNumber == 112)
                        m_GreenCymbal = true;
                    if (note.NoteNumber == 111)
                        m_BlueCymbal = true;
                    if (note.NoteNumber == 110)
                        m_YellowCymbal = true;

                    if (note.NoteNumber == greenNoteNum)
                    {
                        m_GreenLane.AddNote(CreateNote(note));
                        m_GreenCymbal = false;
                    }
                    if (note.NoteNumber == blueNoteNum)
                    {
                        m_BlueLane.AddNote(CreateNote(note));
                        m_BlueCymbal = false;
                    }
                    if (note.NoteNumber == yellowNoteNum)
                    {
                        m_YellowLane.AddNote(CreateNote(note));
                        m_YellowCymbal = false;
                    }
                    if (note.NoteNumber == redNoteNum)
                        m_RedLane.AddNote(CreateNote(note));
                    if (note.NoteNumber == orangeNoteNum)
                        m_OrangeLane.AddNote(CreateNote(note));
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

        noteNew.isSolo = false;
        noteNew.isOverDrive = false;
        noteNew.isHopo = false;
        noteNew.isCymbal = false;
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

        if (m_GreenCymbal || m_BlueCymbal || m_YellowCymbal)
            noteNew.isCymbal = true;

        return noteNew;
    }
}
