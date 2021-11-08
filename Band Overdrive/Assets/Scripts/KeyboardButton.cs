using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
    private int m_PressingCount;
    private bool m_IsPressed;
    private Vector3 m_ButtonPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_IsPressed = false;
        m_PressingCount = 0;
        m_ButtonPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PressingCount > 0)
            m_IsPressed = true;
        else
            m_IsPressed = false;

        if (m_IsPressed)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            gameObject.transform.position = m_ButtonPosition + new Vector3(0.0f, -0.01f, 0.0f);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            gameObject.transform.position = m_ButtonPosition;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (IsFingerTip(col))
        {
            m_PressingCount++;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (IsFingerTip(col))
        {
            m_PressingCount--;
            if (m_PressingCount < 0)
                m_PressingCount = 0;
        }
    }

    private bool IsFingerTip(Collider col)
    {
        string name = col.gameObject.name;
        if (name.Contains("3_CapsuleCollider"))
            return true;
        else 
            return false;
    }

    public void SetUnpressed()
    {
        m_PressingCount = 0;
    }

    public bool IsPressed()
    {
        return m_IsPressed;
    }
}
