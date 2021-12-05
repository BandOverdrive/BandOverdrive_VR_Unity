using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelected : MonoBehaviour
{
    public Dropdown LevelDropdown;
    public string levelSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelSelected = LevelDropdown.options[LevelDropdown.value].text;
    }




}
