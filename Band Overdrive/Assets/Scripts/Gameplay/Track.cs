using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Core;

public class Track : MonoBehaviour
{
    public string m_SongAssetsPath = "InMyPlace";
    public float m_StartDelay = 0.0f;
    public double m_NotesDelay = 0.15f;
    public float m_NoteDistance = 0.35f;

    public enum Level
    {
        Easy,
        Medium,
        Hard,
        Expert
    }
    public Level m_CurrentLevel = Level.Expert;

    protected MidiFile m_MidiFile;
    protected AudioSource m_SongAudio;
    protected ICollection<Melanchall.DryWetMidi.Interaction.Note> m_Notes;

    private string m_ASSETSPATH = Application.streamingAssetsPath + "/Songs/";

    // Start is called before the first frame update
    void Start()
    {
        m_SongAudio = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void LoadSong()
    {
        m_ASSETSPATH += m_SongAssetsPath;

        if (!m_SongAudio)
            m_SongAudio = gameObject.AddComponent<AudioSource>();
        m_SongAudio.clip = (AudioClip)Resources.Load(m_ASSETSPATH + "audio.ogg");

        m_MidiFile = MidiFile.Read(m_ASSETSPATH + "notes.mid");
    }
}
