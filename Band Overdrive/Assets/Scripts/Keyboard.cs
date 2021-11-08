using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public KeyboardButton[] m_KeyButtons;
    public bool[] m_IsNotesPressed;   // TODO: will apply to Note Object Type

    //public KeyboardButton m_Key_G3;
    //public KeyboardButton m_Key_G4;
    //public KeyboardButton m_Key_A3;
    //public KeyboardButton m_Key_A4;
    //public KeyboardButton m_Key_D3;
    //public KeyboardButton m_Key_B3;
    //public KeyboardButton m_Key_F3;
    //public KeyboardButton m_Key_E3;
    //public KeyboardButton m_Key_C3;
    //public KeyboardButton m_Key_D4;
    //public KeyboardButton m_Key_F4;
    //public KeyboardButton m_Key_E4;
    //public KeyboardButton m_Key_B4;
    //public KeyboardButton m_Key_C4;
    //public KeyboardButton m_Key_C5;

    public OVRSkeleton m_LeftHandSkeleton;
    public OVRSkeleton m_RightHandSkeleton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LeftHandSkeleton.gameObject.GetComponent<OVRHand>().IsDataHighConfidence &&
            !m_RightHandSkeleton.gameObject.GetComponent<OVRHand>().IsDataHighConfidence)
        {
            for (int i = 0; i < m_KeyButtons.Length; i++)
            {
                m_KeyButtons[i].SetUnpressed();
            }
        }

        for (int i = 0; i < m_KeyButtons.Length; i++)
        {
            if (m_KeyButtons[i].IsPressed())
                m_IsNotesPressed[i] = true;
            else
                m_IsNotesPressed[i] = false;
        }
    }
}
