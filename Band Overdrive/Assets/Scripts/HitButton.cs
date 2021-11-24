using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour
{
    private bool m_isPressed = false;

    public void setPressed(bool isPressed)
    {
        m_isPressed = isPressed;
    }

    public bool IsPressed()
    {
        return m_isPressed;
    }
}
