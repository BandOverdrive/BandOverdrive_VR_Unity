using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumButton : MonoBehaviour
{
    private bool m_isPressed = false;
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPressed(bool isPressed)
    {
        m_isPressed = isPressed;
    }

    public bool IsPressed()
    {
        return m_isPressed;
    }


}
