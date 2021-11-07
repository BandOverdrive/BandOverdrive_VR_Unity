using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumkitPlayer : MonoBehaviour
{
    public enum Controller
    {
        Left,
        Right
    };

    public Controller m_Controller = Controller.Left;
    public GameObject m_KickSurface;

    private AudioSource m_KickAudio;

    // Start is called before the first frame update
    void Start()
    {
        m_KickAudio = m_KickSurface.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kick
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            m_KickAudio.Play();

            // Generate KICK NOTE
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Hit detection
        OVRInput.Controller c = OVRInput.Controller.LTouch;
        if (m_Controller == Controller.Right)
            c = OVRInput.Controller.RTouch;
        float yVelocoity = OVRInput.GetLocalControllerVelocity(c).y;
        float magnitude = OVRInput.GetLocalControllerVelocity(c).magnitude;
        if (yVelocoity >= -0.05f)
            return;
        if (magnitude < 0.2f)
            return;

        // Sound
        other.gameObject.GetComponent<AudioSource>().Play();

        // Note Generation
        string name = other.gameObject.name;
        switch (name)
        {
            case "S_SNARE":
                // RED NOTE
                break;
            case "S_TOM1":
                // YELLOW NOTE
                break;
            case "S_TOM2":
            case "S_LFLOOR":
                // BLUE NOTE
                break;
            case "S_FLOOR1":
            case "S_FLOOR2":
                // GREEN NOTE
                break;
            case "S_HIHAT":
            case "S_CRASH1":
                // YELLOW ROUNDED NOTE
                break;
            case "S_RIDE":
            case "S_LCRASH":
            case "S_CHINA":
                // BLUE ROUNDED NOTE
                break;
            case "S_CRASH2":
                // GREEN ROUNDED NOTE
                break;

            default:
                break;
        }
    }
}
