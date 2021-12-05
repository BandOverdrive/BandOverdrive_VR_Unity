using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelected : MonoBehaviour
{
    public string SelectedInstrName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrumSceneBtnSelected()
    {
        SelectedInstrName = "Drum";
    }

    public void GuitarSceneBtnSelected()
    {
        SelectedInstrName = "Guitar";
    }

    public void KeyboardSceneBtnSelected()
    {
        SelectedInstrName = "Keyboard";
    }

    public void VocalBtnSelected()
    {
        SelectedInstrName = "Vocal";
    }
}
