using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarPlayer : MonoBehaviour
{
    public Transform m_CenterEyeTransform;
    public OVRSkeleton m_LeftHandSkeleton;
    public GameObject m_Guitar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Position
        Vector3 guitarPos = m_CenterEyeTransform.transform.position;
        float posY = m_CenterEyeTransform.transform.position.y;
        guitarPos.x = transform.position.x;
        guitarPos.z = transform.position.z;
        guitarPos.x += 0.06f;
        guitarPos.y -= 0.3f * posY;
        m_Guitar.transform.position = guitarPos;

        // Rotation
        int boneID = (int)OVRPlugin.BoneId.Hand_Index1;
        if (m_LeftHandSkeleton.Bones.Count > boneID)
        {
            OVRBone posBone = m_LeftHandSkeleton.Bones[boneID];
            if (m_LeftHandSkeleton.gameObject.GetComponent<OVRHand>().IsDataHighConfidence)
            {
                Vector3 lThumbPos = posBone.Transform.position;
                lThumbPos.y += 0.05f;
                Vector3 lHandDir = lThumbPos - guitarPos;
                if (lHandDir.y > -0.05f)
                {
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, lHandDir);
                    rot = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, 100);
                    m_Guitar.transform.rotation = rot;
                }
            }
        }
        else
            m_Guitar.transform.rotation = Quaternion.Euler(-20, -90, 105);
    }
}
