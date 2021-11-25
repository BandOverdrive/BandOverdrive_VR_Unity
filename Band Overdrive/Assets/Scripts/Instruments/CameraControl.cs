using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float m_YDownTime;

    // Start is called before the first frame update
    void Start()
    {
        m_YDownTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Recenter camera
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
            m_YDownTime = Time.time;
        if (OVRInput.Get(OVRInput.RawButton.Y))
        {
            float yTime = Time.time - m_YDownTime;
            if (yTime > 1.5f)
            {
                OVRManager.display.RecenterPose();
                m_YDownTime = Time.time;
            }
        }
    }
}
