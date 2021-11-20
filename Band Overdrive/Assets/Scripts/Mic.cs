using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PitchDetector;

public class Mic : MonoBehaviour
{
    public MicrophonePitchDetector m_PitchDetector;
    public float m_DBTreshold = -30;

    public GameObject m_PitchIndicator;

    // Start is called before the first frame update
    void Start()
    {
        m_PitchDetector.Record = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GeneratePitch(List<float> pitchList, int samples, float db)
    {
        if (pitchList.Count > 0 && db > m_DBTreshold)
        {
            var midis = RAPTPitchDetectorExtensions.HerzToMidi(pitchList);
            int midi = midis[0];
            if (midi >= 36 && midi <= 84)
            {
                // Generate pitch note
                m_PitchIndicator.GetComponent<Renderer>().enabled = true;
                float yPos = ((float)(midi - 36) / 48.0f) * 10.0f;
                Vector3 pos = m_PitchIndicator.transform.localPosition;
                pos = new Vector3(pos.x, pos.y, yPos - 5.0f);
                m_PitchIndicator.transform.localPosition = pos;
            }
            else
                m_PitchIndicator.GetComponent<Renderer>().enabled = false;
        }
        else
            m_PitchIndicator.GetComponent<Renderer>().enabled = false;
    }
}
