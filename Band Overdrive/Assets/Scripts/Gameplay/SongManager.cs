using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds; // delay second for playing song
    public double marginOfError;  // in seconds
    public double delay;  // user input delay time

    public string fileLocation;
    public float noteTime;
    public float noteSpawnZ;  // attention: guitar is x!
    public float noteTapZ;

    ICollection<Melanchall.DryWetMidi.Interaction.Note> drumNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> bassNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> guitarNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> vocalNotes;

    ICollection<Melanchall.DryWetMidi.Interaction.Note> keyExpertNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> keyHardNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> keyMediumNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> keyEasyNotes;

    public enum Instrument
    {
        Drum,
        Bass,
        Guitar,
        Vocal,
        Keyboard
    }
    public Instrument currInstrument;

    public enum Level
    {
        Easy,
        Medium,
        Hard,
        Expert
    }
    public Level currLevel;

    public float noteDespawnZ
    {
        get
        {
            return noteTapZ - (noteSpawnZ - noteTapZ);
        }
    }

    public static MidiFile midiFile; 

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ReadFromFile();
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var chunks = midiFile.GetTrackChunks();

        //notes.CopyTo(array, 0);
        int index = 0;
        foreach (var chunk in chunks)
        {
            if (index == 1)
            {
                // Part Drum
                drumNotes = chunk.GetNotes();
            }
            else if (index == 2)
            {
                // Part Bass
                bassNotes = chunk.GetNotes();
            }
            else if (index == 3)
            {
                // Part Guitar
                guitarNotes = chunk.GetNotes();
            }
            else if (index == 4)
            {
                // Part Vocal
                vocalNotes = chunk.GetNotes();
            }
            else if (index == 8)
            {
                // Part Real Key Expert
                keyExpertNotes = chunk.GetNotes();
            }
            else if (index == 9)
            {
                // Part Real Key Hard
                keyHardNotes = chunk.GetNotes();
            }
            else if (index == 10)
            {
                // Part Real Key Medium
                keyMediumNotes = chunk.GetNotes();
            }
            else if (index == 11)
            {
                // Part Real Key Easy
                keyEasyNotes = chunk.GetNotes();
            }

            index++;
        }

        // split note by level realted to the specific instrucment
        Melanchall.DryWetMidi.Interaction.Note[] levelNotes;
        switch (currInstrument)
        {
            case Instrument.Drum:
                print("----- drum -----");
                levelNotes = getNotesByLevel(drumNotes, currLevel);
                setNotesToLanes(levelNotes);
                break;
            case Instrument.Bass:
                print("----- bass -----");
                levelNotes = getNotesByLevel(bassNotes, currLevel);
                setNotesToLanes(levelNotes);
                break;
            case Instrument.Guitar:
                print("----- guitar -----");
                levelNotes = getNotesByLevel(guitarNotes, currLevel);
                setNotesToLanes(levelNotes);
                break;
            case Instrument.Vocal:
                print("----- vocal -----");
                levelNotes = getNotesByLevel(vocalNotes, currLevel);
                setNotesToLanes(levelNotes);
                break;
            case Instrument.Keyboard:
                print("----- keyboard -----");
                levelNotes = getKeyNoteByLevel(currLevel);
                setNotesToLanes(levelNotes);
                break;
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    private Melanchall.DryWetMidi.Interaction.Note[] getNotesByLevel(ICollection<Melanchall.DryWetMidi.Interaction.Note> notes, Level currLevel)
    {
        List<Melanchall.DryWetMidi.Interaction.Note> levelNotes = new List<Melanchall.DryWetMidi.Interaction.Note>();
        switch (currLevel)
        {
            case Level.Easy:
                print("===== EASY(4) =====");
                foreach (var note in notes)
                    if (note.Octave == 4) levelNotes.Add(note);
                break;

            case Level.Medium:
                print("===== EASY(5) =====");
                foreach (var note in notes)
                    if (note.Octave == 5) levelNotes.Add(note);
                break;

            case Level.Hard:
                print("===== EASY(6) =====");
                foreach (var note in notes)
                    if (note.Octave == 6) levelNotes.Add(note);
                break;

            case Level.Expert:
                print("===== EASY(7) =====");
                foreach (var note in notes)
                    if (note.Octave == 7) levelNotes.Add(note);
                break;
        }

        return levelNotes.ToArray();
    }

    private Melanchall.DryWetMidi.Interaction.Note[] getKeyNoteByLevel(Level currLevel)
    {

        if (currLevel == Level.Expert)
        {
            Melanchall.DryWetMidi.Interaction.Note[] levelNotes = new Melanchall.DryWetMidi.Interaction.Note[keyExpertNotes.Count];
            keyExpertNotes.CopyTo(levelNotes, 0);
            return levelNotes;
        }
        else if (currLevel == Level.Hard)
        {
            Melanchall.DryWetMidi.Interaction.Note[] levelNotes = new Melanchall.DryWetMidi.Interaction.Note[keyHardNotes.Count];
            keyHardNotes.CopyTo(levelNotes, 0);
            return levelNotes;
        }
        else if (currLevel == Level.Medium)
        {
            Melanchall.DryWetMidi.Interaction.Note[] levelNotes = new Melanchall.DryWetMidi.Interaction.Note[keyMediumNotes.Count];
            keyMediumNotes.CopyTo(levelNotes, 0);
            return levelNotes;
        }
        else if (currLevel == Level.Easy)
        {
            Melanchall.DryWetMidi.Interaction.Note[] levelNotes = new Melanchall.DryWetMidi.Interaction.Note[keyEasyNotes.Count];
            keyEasyNotes.CopyTo(levelNotes, 0);
            return levelNotes;
        }
        else
        {
            return null;
        }
    }

    private void setNotesToLanes(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach (var lane in lanes)
        {
            lane.SetTimeStamps(currInstrument, notes, delay);
        }
    }

    public void StartSong()
    {
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
