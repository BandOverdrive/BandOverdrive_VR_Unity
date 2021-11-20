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
    public Animator m_KickAnimator;
    public HihatControl m_HihatControl;

    private AudioSource m_KickAudio;

    // Start is called before the first frame update
    void Start()
    {
        m_KickAudio = m_KickSurface.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Controller == Controller.Right)
        {
            // Kick
            if (Input.GetKeyDown(KeyCode.Space)
                || OVRInput.GetDown(OVRInput.Button.One))
            {
                m_KickAudio.Play();
                m_KickAnimator.Play("Drum_KickBeat");
                // Generate KICK NOTE
            }

            // Hihat
            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
                m_HihatControl.Close();
            if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
                m_HihatControl.Open();
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

        // Haptics
        if (magnitude > 1.0f)
            magnitude = 1.0f;
        Haptics(1, magnitude, 0.03f, c);

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
                if (m_HihatControl.IsOpened())
                {
                    // BLUE ROUNDED NOTE
                }
                else
                {
                    // YELLOW ROUNDED NOTE
                }
                break;
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

    private IEnumerator VibrateRoutine(float frequency, float amplitude, float duration,
        OVRInput.Controller controllerMask = OVRInput.Controller.Active)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controllerMask);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controllerMask);
    }

    private void Haptics(float frequency, float amplitude, float duration,
        OVRInput.Controller controllerMask = OVRInput.Controller.Active)
    {
        StartCoroutine(VibrateRoutine(frequency, amplitude, duration, controllerMask));
    }
}
