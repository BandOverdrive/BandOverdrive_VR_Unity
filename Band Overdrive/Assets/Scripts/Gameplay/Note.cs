using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    //private double m_HitTime;
    private bool m_HasTail;
    private double m_DeltaTime;
    private float m_TailLength;

    private bool m_IsSolo;
    private bool m_IsOverDrive;
    private bool m_IsHopo;
    private bool m_IsCymbal;

    private double m_SpawnTime;
    private bool m_IsSpawned;
    private bool m_IsHit;
    private bool m_IsPressing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsSpawned)
            return;

        Track track = transform.parent.parent.GetComponent<Track>();

        double timeSinceSpawn = track.GetTimeElapsed() - m_SpawnTime;
        float posAlpha = (float)(timeSinceSpawn / (track.m_NoteRollTime * 2.0f));
        double tailtimeSinceSpawn = timeSinceSpawn - m_DeltaTime;
        float tailAlpha = (float)(tailtimeSinceSpawn / (track.m_NoteRollTime * 2.0f));

        if (posAlpha < 1.0f || (m_HasTail && (tailAlpha < 1.0f)))
        {
            if (!m_IsHit)
                GetComponent<Renderer>().enabled = true;
            else
                GetComponent<Renderer>().enabled = false;
            if (m_HasTail)
                transform.GetChild(0).gameObject.SetActive(true);

            Vector3 direction = new Vector3(0, 0, 1);
            Vector3 posBegin = direction * track.m_NoteRollDistance;
            Vector3 posEnd = direction * -track.m_NoteRollDistance;
            transform.localPosition = Vector3.LerpUnclamped(posBegin, posEnd, posAlpha);
        }
        else
            Destroy(gameObject);
    }

    public void SetTail(double deltaTime)
    {
        m_HasTail = true;
        Track track = transform.parent.parent.GetComponent<Track>();
        float speed = track.m_NoteRollDistance / track.m_NoteRollTime;
        m_TailLength = speed * (float)deltaTime / track.m_NoteTailScale;
        m_DeltaTime = deltaTime;

        transform.GetChild(0).localPosition = new Vector3(0.0f, 0.0f, m_TailLength);
        transform.GetChild(0).localScale =
            new Vector3(0.2f, track.m_NoteTailWidth, m_TailLength);
    }

    public void UpdateTail(double deltaTime)
    {
        Track track = transform.parent.parent.GetComponent<Track>();
        float speed = track.m_NoteRollDistance / track.m_NoteRollTime;
        float length = speed * (float)deltaTime / 0.005f;
        transform.GetChild(0).localScale =
            new Vector3(0.2f, track.m_NoteTailWidth, length);
        m_IsPressing = true;
    }

    public void StopPressing()
    {
        m_IsPressing = false;
    }

    public void SetSolo(bool solo)
    {
        m_IsSolo = solo;
    }

    public void SetOverDrive(bool overDrive)
    {
        m_IsOverDrive = overDrive;
    }

    public void SetHopo(bool hopo)
    {
        m_IsHopo = hopo;
    }

    public void SetCymbal(bool cymbal)
    {
        m_IsCymbal = cymbal;
    }

    public void Spawn(double spawnTime)
    {
        m_SpawnTime = spawnTime;
        m_IsSpawned = true;
        m_IsHit = false;
        m_IsPressing = false;
    }

    public void Hit()
    {
        m_IsHit = true;
    }

    public bool IsHit()
    {
        return m_IsHit;
    }
}
