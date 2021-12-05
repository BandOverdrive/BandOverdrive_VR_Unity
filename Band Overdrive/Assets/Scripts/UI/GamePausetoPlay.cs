using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausetoPlay : MonoBehaviour
{
    public GameObject canvas_Stop;
    public GameObject UIhelper;

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseToPlay()
    {
            canvas_Stop.SetActive(false);
            UIhelper.SetActive(false);
            ResumeGame();
    }
}
