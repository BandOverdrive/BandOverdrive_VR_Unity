using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SongSelected : MonoBehaviour
{
    public string songSelected = "Yellow";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void SelectedSong1(bool isOn)
    {
        songSelected = "InMyPlace"; 
    }

    public void SelectedSong2(bool isOn)
    {
        songSelected = "Yellow";
    }
}
