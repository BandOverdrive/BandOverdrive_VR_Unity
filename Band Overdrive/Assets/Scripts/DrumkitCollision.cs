using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumkitCollision : MonoBehaviour
{
    AudioSource audioSource;

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
        if (col.gameObject.name == "Drum")
        {
            print("test stestse");
            audioSource = col.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}
