using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction_instruSingle : MonoBehaviour
{
    public GameObject canvas_instruSingle;
    public GameObject canvas_songSingle;

    public GameObject camera_main;
    public GameObject camera_0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Set UI scene from single_Instrument to single_Song
    public void SetSongActive_single()
    {
        canvas_instruSingle.SetActive(false);
        canvas_songSingle.SetActive(true);
        camera_main.transform.position = camera_0.transform.position;
    }


}
