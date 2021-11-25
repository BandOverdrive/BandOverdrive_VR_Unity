using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButton : HitButton
{
    public OVRHand m_LeftHand;
    public OVRHand m_RightHand;

    private int m_PressingCountL;
    private int m_PressingCountR;
    private int m_PressingCount;
    private bool m_IsHitted;
    private float m_HitEffectInSeconds = 0.5f;

    private GameObject m_keyMesh;
    private Vector3 m_ButtonPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_PressingCountL = 0;
        m_PressingCountR = 0;
        m_PressingCount = 0;
        m_IsHitted = false;

        m_keyMesh = gameObject.transform.GetChild(0).gameObject;
        m_ButtonPosition = m_keyMesh.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_LeftHand.IsDataHighConfidence)
            m_PressingCountL = 0;
        if (!m_RightHand.IsDataHighConfidence)
            m_PressingCountR = 0;

        m_PressingCount = m_PressingCountL + m_PressingCountR;

        if (m_PressingCount > 0)
            m_IsHitted = true;
        else
            m_IsHitted = false;

        if (m_IsHitted)
        {
            //m_keyMesh.GetComponent<Renderer>().material.color = Color.blue;
            m_keyMesh.transform.position = m_ButtonPosition + new Vector3(0.0f, -0.01f, 0.0f);
        }
        else
        {
            //m_keyMesh.GetComponent<Renderer>().material.color = Color.white;
            m_keyMesh.transform.position = m_ButtonPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFingerTip(other))
        {
            if (m_PressingCount == 0)
            {
                // Hit the note (now using press as the signal)
                setPressed(true);
                Invoke(nameof(setUnPressed), m_HitEffectInSeconds);
            }
            if (IsLeftHand(other))
                m_PressingCountL++;
            else
                m_PressingCountR++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsFingerTip(other))
        {
            if (IsLeftHand(other))
                m_PressingCountL--;
            else
                m_PressingCountR--;

            if (m_PressingCountL < 0)
                m_PressingCountL = 0;
            if (m_PressingCountR < 0)
                m_PressingCountR = 0;
        }
    }

    private bool IsFingerTip(Collider collider)
    {
        string name = collider.gameObject.name;
        if (name.Contains("3_CapsuleCollider"))
            return true;
        else 
            return false;
    }

    private bool IsLeftHand(Collider collider)
    {
        GameObject handPrefab = collider.transform.parent.parent.parent.gameObject;
        OVRSkeleton.SkeletonType type =
            handPrefab.GetComponent<OVRSkeleton>().GetSkeletonType();
        if (type == OVRSkeleton.SkeletonType.HandLeft)
            return true;
        else
            return false;
    }
}
