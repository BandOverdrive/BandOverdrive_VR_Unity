using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Toggle m_Toggle_InMyPlace;
    public Toggle m_Toggle_Yellow;
    public Dropdown m_Dropdown_Level;

    public string m_SongSelected = "Yellow";
    public string m_LevelSelected = "Easy";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // update the song selection
        if (m_Toggle_Yellow.isOn)
            m_SongSelected = "Yellow";
        else if (m_Toggle_InMyPlace)
            m_SongSelected = "InMyPlace";

        // update the level dropdown selection
        string levelSelected = m_Dropdown_Level.options[m_Dropdown_Level.value].text;
        if (!string.IsNullOrEmpty(levelSelected))
            m_LevelSelected = levelSelected;
    }
}
