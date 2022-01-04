using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonInteraction_home : MonoBehaviour
{
    public GameObject canvas_home;
    public GameObject canvas_instruSingle;
    public GameObject canvas_instruMulti;
    public GameObject canvas_setting;

    public GameObject camera_main;
    public GameObject camera_1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set UI scene from HOME to single_Instrument
    public void SetInstrucActive_single()
    {
        canvas_instruSingle.SetActive(true);
        canvas_home.SetActive(false);
        camera_main.transform.position = camera_1.transform.position;
    }

    //Set UI scene from HOME to multi_Instrument
    public void LoadMultiplayerScene()
    {
        // Load the multiplayer loading scene
        SceneManager.LoadScene(6);
    }

    //Set UI scene from HOME to setting
    public void SetSettingActive()
    {
        canvas_setting.SetActive(true);
        canvas_home.SetActive(false);
    }


}
