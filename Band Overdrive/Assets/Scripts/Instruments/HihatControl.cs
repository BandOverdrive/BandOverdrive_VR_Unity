using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HihatControl : MonoBehaviour
{
    public Animator m_HihatAnimator;

    public GameObject m_HihatSurface;
    public AudioClip m_OpenedHihatAudio;
    public AudioClip m_ClosedHihatAudio;

    private bool m_IsOpened;

    // Start is called before the first frame update
    void Start()
    {
        m_IsOpened = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsOpened()
    {
        return m_IsOpened;
    }

    public void Open()
    {
        m_HihatAnimator.Play("Drum_HihatOpen");
        m_IsOpened = true;
        m_HihatSurface.GetComponent<AudioSource>().clip = m_OpenedHihatAudio;
    }

    public void Close()
    {
        m_HihatAnimator.Play("Drum_HihatClose");
        m_IsOpened = false;
        m_HihatSurface.GetComponent<AudioSource>().clip = m_ClosedHihatAudio;
    }
}
