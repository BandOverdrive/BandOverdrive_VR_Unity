using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour
{
    protected bool m_Hit;
    protected bool m_IsPressed;

    // Start is called before the first frame update
    void Start()
    {
        m_Hit = false;
        m_IsPressed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    private void HitEnd()
    {
        m_Hit = false;
    }

    public bool IsHit()
    {
        return m_Hit;
    }

    public bool IsPressed()
    {
        return m_IsPressed;
    }
}
