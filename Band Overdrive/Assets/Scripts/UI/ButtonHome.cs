using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHome : MonoBehaviour
{
    public GameObject canvas_now;
    public GameObject canvas_home;

    public GameObject camera_main;
    public GameObject camera_home;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHomeActive()
    {
        canvas_now.SetActive(false);
        canvas_home.SetActive(true);
        camera_main.transform.position = camera_home.transform.position;
    }
}
