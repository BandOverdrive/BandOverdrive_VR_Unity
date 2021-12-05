using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRoleDisplay : MonoBehaviour
{
    public GameObject StarDrum;
    public GameObject StarGuitar;
    public GameObject StarKeyboard;
    public GameObject StarVocal;

    public Sprite ImageDrum;
    public Sprite ImageGuitar;
    public Sprite ImageKeyboard;
    public Sprite ImageVocal;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public GameObject Star4;

    // Start is called before the first frame update
    void Start()
    {
        string CurrentInstrSelected = SceneLoadManager.instrPasstoScene;

        if (CurrentInstrSelected == "Drum")
        {
            StarDrum.GetComponent<Image>().sprite = ImageDrum;
            Star1.SetActive (true);
        }
        else if (CurrentInstrSelected == "Guitar")
        {
            StarGuitar.GetComponent<Image>().sprite = ImageGuitar;
            Star2.SetActive(true);
        }
        else if (CurrentInstrSelected == "Keyboard")
        {
            StarKeyboard.GetComponent<Image>().sprite = ImageKeyboard;
            Star3.SetActive(true);
        }
        else if (CurrentInstrSelected == "Vocal")
        {
            StarVocal.GetComponent<Image>().sprite = ImageVocal;
            Star4.SetActive(true);
        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
