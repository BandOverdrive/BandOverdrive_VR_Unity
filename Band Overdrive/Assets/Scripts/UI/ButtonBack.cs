using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public GameObject canvas_now;
    public GameObject canvas_previous;

    public GameObject camera_main;
    public GameObject camera_previous;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBackActive()
    {
        canvas_now.SetActive(false);
        canvas_previous.SetActive(true);
        camera_main.transform.position = camera_previous.transform.position;
    }
}
