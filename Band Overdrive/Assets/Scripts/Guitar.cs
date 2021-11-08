using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
{
    public GuitarButton m_GreenButton;
    public GuitarButton m_RedButton;
    public GuitarButton m_YellowButton;
    public GuitarButton m_BlueButton;
    public GuitarButton m_OrangeButton;

    public OVRHand m_LeftHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LeftHand.IsDataHighConfidence)
        {
            m_GreenButton.SetUnpressed();
            m_RedButton.SetUnpressed();
            m_YellowButton.SetUnpressed();
            m_BlueButton.SetUnpressed();
            m_OrangeButton.SetUnpressed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        if (name.Contains("Hand_Thumb3_CapsuleCollider"))
        {
            if (m_GreenButton.IsPressed())
            {
                // GREEN NOTE
            }
            if (m_RedButton.IsPressed())
            {
                // RED NOTE
            }
            if (m_YellowButton.IsPressed())
            {
                // YELLOW NOTE
            }
            if (m_BlueButton.IsPressed())
            {
                // BLUE NOTE
            }
            if (m_OrangeButton.IsPressed())
            {
                // ORANGE NOTE
            }
        }
    }
}
