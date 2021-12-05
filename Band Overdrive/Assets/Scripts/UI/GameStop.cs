using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStop : MonoBehaviour
{
    public GameObject canvas_Stop;
    public GameObject instruTrack;
    public GameObject UIhelper;

    public void PauseGame()
    {
        Time.timeScale = 0;
        instruTrack.GetComponent<Track>().enabled = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UI
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            canvas_Stop.SetActive(true);
            UIhelper.SetActive(true);
            //PauseGame();
        }



    }
}
