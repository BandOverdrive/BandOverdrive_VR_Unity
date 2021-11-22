using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;

public class SongManagerTest : MonoBehaviour
{
    public static SongManagerTest Instance;
    public AudioSource audioSource;
    public static MidiFile midiFile;
    public Lane[] lanes;
    public float songDelayInSeconds; // delay second for playing song
    //public double marginOfError;  // in seconds
    //public int inputDelayInMilliseconds;  // keyboard input delay time

    public string fileLocation;
    //public float noteTime;
    //public float noteSpawnX;
    //public float noteTapX;

    ICollection<Melanchall.DryWetMidi.Interaction.Note> drumNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> bassNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> guitarNotes;
    ICollection<Melanchall.DryWetMidi.Interaction.Note> vocalNotes;

    public enum Instrument { 
        Drum, 
        Bass, 
        Guitar, 
        Vocal
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

    //public float noteDespawnX
    //{
    //    get
    //    {
    //        return noteTapX - (noteSpawnX - noteTapX);
    //    }
    //}



    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFileFromWebSite());
        }
        else
        {
            ReadFromFile();
        }
    }

    private IEnumerator ReadFileFromWebSite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        //var notes = midiFile.GetNotes();
        //var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
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

    private void setNotesToLanes(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach (var note in notes)
        {
            print("----- Note in Easy----- " + note);
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
