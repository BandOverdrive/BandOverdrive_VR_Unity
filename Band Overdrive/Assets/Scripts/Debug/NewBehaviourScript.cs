using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Tooltip("Activate the script")]
    public bool m_Activated = false;
    [Tooltip("This is the ID number assigned to this script")]
    public int m_IdNumber = 0;

    private int m_Id;   // Private variable

    // Start is called before the first frame update
    void Start()
    {
        m_Id = m_IdNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Activated)
            ModifyID(1);
    }

    private void ModifyID(int n)
    {
        m_Id = m_IdNumber + n;
    }

    public int GetID()
    {
        return m_Id;
    }
}
