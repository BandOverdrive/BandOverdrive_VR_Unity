using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime; // the time the note is supposed to be tapped
    
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        GetComponent<Renderer>().enabled = true;
        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.localPosition = Vector3.Lerp(Vector3.right * SongManager.Instance.noteSpawnX, Vector3.right * SongManager.Instance.noteDespawnX, t);
        }
    }
}
