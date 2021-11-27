using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour
{
    protected bool m_Hit;
    protected bool m_IsPressed;

    protected bool m_IsTomHit;
    protected bool m_IsHopoHit;

    // Start is called before the first frame update
    void Start()
    {
        m_Hit = false;
        m_IsPressed = false;

        ButtonStart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void ButtonStart()
    {

    }

    public void Hit()
    {
        m_IsTomHit = false;
        m_IsHopoHit = false;

        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    public void TomHit()
    {
        m_IsTomHit = true;
        m_IsHopoHit = false;

        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    public void HopoHit()
    {
        m_IsTomHit = false;
        m_IsHopoHit = true;

        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    private void HitEnd()
    {
        m_Hit = false;

        m_IsTomHit = false;
        m_IsHopoHit = false;
    }

    public bool IsHit()
    {
        return m_Hit;
    }

    public bool IsPressed()
    {
        return m_IsPressed;
    }

    public bool IsTomHit()
    {
        return m_IsTomHit;
    }

    public bool IsHopoHit()
    {
        return m_IsHopoHit;
    }
}
