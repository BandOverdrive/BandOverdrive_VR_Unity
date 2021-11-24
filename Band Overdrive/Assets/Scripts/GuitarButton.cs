using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarButton : HitButton
{
    //private bool m_IsPressed;
    private int m_PressingCount;

    // Start is called before the first frame update
    void Start()
    {
        setPressed(false);
        m_PressingCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PressingCount > 0)
            setPressed(true);
        else
            setPressed(false);

        if (IsPressed())
            gameObject.GetComponent<Animator>().Play("Press");
        else
            gameObject.GetComponent<Animator>().Play("Idle");
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
        {
            if (!name.Contains("Hand_Thumb3_CapsuleCollider"))
                return true;
        }
        return false;
    }

    public void SetUnpressed()
    {
        m_PressingCount = 0;
    }

    //public bool IsPressed()
    //{
    //    return m_IsPressed;
    //}
}
