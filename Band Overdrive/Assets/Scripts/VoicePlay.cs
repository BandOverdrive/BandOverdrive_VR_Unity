using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PitchDetector;

public class VoicePlay : MonoBehaviour
{

    public MicrophonePitchDetector detector;

    // Start is called before the first frame update
    void Start()
    {
        detector.Record = true;
    }

    // Update is called once per frame
    void Update()
    {
        detector.onPitchDetected.AddListener(LogPitch);
    }

    public void LogPitch(List<float> pitchList, int samples, float db)
    {
        var midis = RAPTPitchDetectorExtensions.HerzToMidi(pitchList);
        Debug.Log("detected " + pitchList.Count + " values from " + samples + " samples, db:" + db);
        Debug.Log(midis.NoteString());
    }


}
