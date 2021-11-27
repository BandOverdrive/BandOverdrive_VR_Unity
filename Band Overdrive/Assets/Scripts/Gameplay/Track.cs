using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;

public class Track : MonoBehaviour
{
    public string m_SongAssetsPath = "InMyPlace";
    public float m_StartDelay = 0.0f;
    public double m_NotesDelay = 0.15f;
    public double m_ErrorTime = 0.15f;

    public float m_NoteRollDistance = 0.35f;
    public float m_NoteRollTime = 4.0f;

    public float m_NoteTailScale = 0.01f;
    public float m_NoteTailWidth = 0.1f;

    public int m_CurrentScore = 0;
    public int m_CurrentHP = 80;
    public int m_CurrentCombo = 0;

    public enum Level
    {
        Easy,
        Medium,
        Hard,
        Expert
    }
    public Level m_CurrentLevel = Level.Expert;

    protected MidiFile m_MidiFile;
    protected Melanchall.DryWetMidi.Interaction.Note[] m_Notes;

    public struct Note
    {
        public double hitTime;
        public bool hasTail;
        public double deltaTime;

        public bool isSolo;
        public bool isOverDrive;
        public bool isHopo;
        public bool isCymbal;

        public int noteNumber;
        public string lyric;
    }

    protected bool m_SoloSection;
    protected long m_CurSoloEndTime;
    protected bool m_ODSection;
    protected long m_CurODEndTime;
    protected bool m_HopoNotes;
    protected long m_CurHopoEndTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentHP > 100)
            m_CurrentHP = 100;
        if (m_CurrentHP <= 0)
        {
            m_CurrentHP = 0;
            // GAMEOVER
        }
    }

    protected void LoadSong()
    {
        string audioResourcePath = "Songs/" + m_SongAssetsPath + "/audio";
        string midiPath = "assets/Resources/Songs/"+ m_SongAssetsPath + "/notes.mid";

        if (!gameObject.GetComponent<AudioSource>())
            gameObject.AddComponent<AudioSource>();
        AudioClip song = Resources.Load<AudioClip>(audioResourcePath);
        gameObject.GetComponent<AudioSource>().clip = song;

        m_MidiFile = MidiFile.Read(midiPath);
    }

    private void StartSong()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    protected void PlaySong()
    {
        Invoke(nameof(StartSong), m_StartDelay);
    }

    public double GetTimeElapsed()
    {
        int timeSamples = gameObject.GetComponent<AudioSource>().timeSamples;
        int frequency = gameObject.GetComponent<AudioSource>().clip.frequency;
        return (double)timeSamples / frequency;
    }
}
