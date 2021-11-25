using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Melanchall.DryWetMidi.Interaction;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    //public KeyCode input; // this is keyboard input
    //public GuitarButton btnInput; // this is vr button input
    public HitButton btnInput;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();  // array of exact time for each note should be tapped

    int spawnIndex = 0;
    int inputIndex = 0;

    void Start()
    {
        
    }

    public void SetTimeStamps(SongManager.Instrument currInstrument, Melanchall.DryWetMidi.Interaction.Note[] array, double delay)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                if (currInstrument == SongManager.Instrument.Keyboard)
                {
                    if (!gameObject.name.Contains(note.Octave.ToString()))
                        continue;
                }
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f + delay);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                // the time for the note to be spawn
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            // simplify the variable to include user input delay
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime() - (SongManager.Instance.delay / 1000f);
        
            if (btnInput.IsHit())
            {
                // hit the button
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    // success to hit the note
                    Hit();
                    print($"Hit successfully on {inputIndex} note!");
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
                else
                {
                    // fail to hit the note with
                    print($"Hit inaccurate on {inputIndex} note");

                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                // note hit the button 
                Miss();
                print($"Miss on {inputIndex} note");
                inputIndex++;
            }
        
        }
    }

    private void Hit()
    {
        ScoreManager.Hit();
    }

    private void Miss()
    {
        ScoreManager.Miss();
    }
}
