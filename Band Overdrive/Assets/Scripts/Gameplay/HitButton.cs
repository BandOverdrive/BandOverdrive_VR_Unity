using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour
{
    protected bool m_Hit;
    protected bool m_IsPressed;

    protected bool m_IsCymbalHit;
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
        m_IsCymbalHit = false;
        m_IsHopoHit = false;

        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    public void CymbalHit()
    {
        m_IsCymbalHit = true;
        m_IsHopoHit = false;

        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    public void HopoHit()
    {
        m_IsCymbalHit = false;
        m_IsHopoHit = true;

        m_Hit = true;
        Invoke(nameof(HitEnd), 0.1f);
    }

    private void HitEnd()
    {
        m_Hit = false;

        m_IsCymbalHit = false;
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

    public bool IsCymbalHit()
    {
        return m_IsCymbalHit;
    }

    public bool IsHopoHit()
    {
        return m_IsHopoHit;
    }
}
