using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalNote : MonoBehaviour
{
    private double m_DeltaTime;

    private bool m_IsOverDrive;

    private double m_SpawnTime;
    private bool m_IsSpawned;

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

        if (tailAlpha < 1.0f)
        {
            GetComponent<Renderer>().enabled = true;
            transform.GetChild(0).GetComponent<Renderer>().enabled = true;

            Vector3 direction = new Vector3(1, 0, 0);
            Vector3 posBegin = direction * track.m_NoteRollDistance;
            Vector3 posEnd = direction * -track.m_NoteRollDistance;
            transform.localPosition = Vector3.LerpUnclamped(posBegin, posEnd, posAlpha);
        }
        else
            Destroy(gameObject);
    }

    public void SetDeltaTime(double deltaTime)
    {
        m_DeltaTime = deltaTime;
    }

    public void SetOverDrive(bool overDrive)
    {
        m_IsOverDrive = overDrive;
    }

    public void Spawn(double spawnTime)
    {
        m_SpawnTime = spawnTime;
        m_IsSpawned = true;
    }
}
