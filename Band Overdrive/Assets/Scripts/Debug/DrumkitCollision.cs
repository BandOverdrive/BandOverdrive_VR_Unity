using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumkitCollision : MonoBehaviour
{
    AudioSource audioSource;
    Vector3 drumScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Drum")
        {
            drumScale = col.gameObject.transform.localScale;
            col.gameObject.transform.localScale = drumScale * 1.1f;
            audioSource = col.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Drum")
        {
            col.gameObject.transform.localScale = drumScale;
        }
    }
}
