using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBacktoHome : MonoBehaviour
{
/*    public SceneSelected SceneBtnSelected;
    public LevelSelected currentLevelSelected;
    public SongSelected currentSongSelected;*/
    public static string levelPasstoScene;
    public static string songPasstoScene;
    public static string instrPasstoScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgain ()
    {
        string CurrentInstrSelected = SceneLoadManager.instrPasstoScene;
        string Selectedlevel = SceneLoadManager.levelPasstoScene;
        string Selectedsong = SceneLoadManager.songPasstoScene;


        if (CurrentInstrSelected == "Drum")
        {
            levelPasstoScene = Selectedlevel;
            songPasstoScene = Selectedsong;
            instrPasstoScene = "Drum";
            SceneManager.LoadScene(1);
        }
        else if (CurrentInstrSelected == "Guitar")
        {
            levelPasstoScene = Selectedlevel;
            songPasstoScene = Selectedsong;
            instrPasstoScene = "Guitar";
            SceneManager.LoadScene(2);
        }
        else if (CurrentInstrSelected == "Keyboard")
        {
            levelPasstoScene = Selectedlevel;
            songPasstoScene = Selectedsong;
            instrPasstoScene = "Keyboard";
            SceneManager.LoadScene(3);
        }
        else if (CurrentInstrSelected == "Vocal")
        {
            levelPasstoScene = Selectedlevel;
            songPasstoScene = Selectedsong;
            instrPasstoScene = "Vocal";
            SceneManager.LoadScene(4);
        }
        else
        {
            levelPasstoScene = Selectedlevel;
            songPasstoScene = Selectedsong;
            instrPasstoScene = "Drum";
            SceneManager.LoadScene(1);
        }
    }



}
