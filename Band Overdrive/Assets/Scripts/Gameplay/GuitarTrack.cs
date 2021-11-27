using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class GuitarTrack : Track
{
    public LaneNew m_GreenLane;
    public LaneNew m_RedLane;
    public LaneNew m_YellowLane;
    public LaneNew m_BlueLane;
    public LaneNew m_OrangeLane;

    public enum Instrument
    {
        Guitar,
        Bass
    }
    public Instrument m_CurrentInstrument = Instrument.Guitar;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(LoadAndPlay), 0.1f);
    }

    private void LoadAndPlay()
    {
        LoadSong();

        int hopoNoteNum;
        int orangeNoteNum;
        int blueNoteNum;
        int yellowNoteNum;
        int redNoteNum;
        int greenNoteNum;
        if (m_CurrentLevel == Level.Expert)
        {
            hopoNoteNum = 101;
            orangeNoteNum = 100;
            blueNoteNum = 99;
            yellowNoteNum = 98;
            redNoteNum = 97;
            greenNoteNum = 96;

            //m_NoteRollTime = A SMALL NUMBER;
        }
        else if (m_CurrentLevel == Level.Hard)
        {
            hopoNoteNum = 89;
            orangeNoteNum = 88;
            blueNoteNum = 87;
            yellowNoteNum = 86;
            redNoteNum = 85;
            greenNoteNum = 84;

            //m_NoteRollTime = A MODERATE NUMBER;
        }
        else if (m_CurrentLevel == Level.Medium)
        {
            hopoNoteNum = 77;
            orangeNoteNum = 76;
            blueNoteNum = 75;
            yellowNoteNum = 74;
            redNoteNum = 73;
            greenNoteNum = 72;

            //m_NoteRollTime = A HIGH NUMBER;
        }
        else
        {
            hopoNoteNum = 65;
            orangeNoteNum = 64;
            blueNoteNum = 63;
            yellowNoteNum = 62;
            redNoteNum = 61;
            greenNoteNum = 60;

            //m_NoteRollTime = A HIGHER NUMBER;
        }

        string trackNameString = "";
        switch (m_CurrentInstrument)
        {
            case Instrument.Guitar:
                trackNameString = "Sequence/Track Name (PART GUITAR)";
                break;
            case Instrument.Bass:
                trackNameString = "Sequence/Track Name (PART BASS)";
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

                    if (note.NoteNumber == hopoNoteNum)
                    {
                        m_HopoNotes = true;
                        m_CurHopoEndTime = note.GetTimedNoteOffEvent().Time;
                    }

                    if (note.NoteNumber == orangeNoteNum)
                        m_OrangeLane.AddNote(CreateNote(note));
                    if (note.NoteNumber == blueNoteNum)
                        m_BlueLane.AddNote(CreateNote(note));
                    if (note.NoteNumber == yellowNoteNum)
                        m_YellowLane.AddNote(CreateNote(note));
                    if (note.NoteNumber == redNoteNum)
                        m_RedLane.AddNote(CreateNote(note));
                    if (note.NoteNumber == greenNoteNum)
                        m_GreenLane.AddNote(CreateNote(note));
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

        if (note.LengthAs(TimeSpanType.Musical, tempoMap).ToString() != "1/16")
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

        if (m_HopoNotes)
        {
            if (note.Time > m_CurHopoEndTime)
                m_HopoNotes = false;
            else
                noteNew.isHopo = true;
        }

        return noteNew;
    }
}
