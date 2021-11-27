using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicCursor : MonoBehaviour
{
    private int m_CurrentPitch = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPitch(int pitch)
    {
        m_CurrentPitch = pitch;
    }

    public int CurrentPitch()
    {
        return m_CurrentPitch;
    }
}
