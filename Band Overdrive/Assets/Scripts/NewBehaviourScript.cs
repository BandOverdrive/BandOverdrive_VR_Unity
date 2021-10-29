using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Tooltip("Activate the script")]
    public bool m_activated = false;
    [Tooltip("This is the ID number assigned to this script")]
    public int m_idNumber = 0;

    private int m_id;   // Private variable

    // Start is called before the first frame update
    void Start()
    {
        m_id = m_idNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_activated)
            ModifyID(1);
    }

    private void ModifyID(int n)
    {
        m_id = m_idNumber + n;
    }

    public int GetID()
    {
        return m_id;
    }
}
