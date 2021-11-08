using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarButton : MonoBehaviour
{
    private bool m_IsPressed;
    private int m_PressingCount;

    // Start is called before the first frame update
    void Start()
    {
        m_IsPressed = false;
        m_PressingCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PressingCount > 0)
            m_IsPressed = true;
        else
            m_IsPressed = false;

        if (m_IsPressed)
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        else
            gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFingerTip(other))
            m_PressingCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsFingerTip(other))
        {
            m_PressingCount--;
            if (m_PressingCount < 0)
                m_PressingCount = 0;
        }
    }

    private bool IsFingerTip(Collider collider)
    {
        string name = collider.gameObject.name;
        if (name.Contains("3_CapsuleCollider"))
            return true;
        return false;
    }

    public void setUnpressed()
    {
        m_PressingCount = 0;
    }

    public bool IsPressed()
    {
        return m_IsPressed;
    }
}
