using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneLoadManager : MonoBehaviour
{
    public SceneSelected SceneBtnSelected;
    public LevelSelected currentLevelSelected;
    public SongSelected currentSongSelected;
    public static string levelPasstoScene;
    public static string songPasstoScene;
    public static string instrPasstoScene;
    public static string scorePasstoScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoad()
    {
        if (StateNameController.gamePlayMode == StateNameController.SINGLE_MODE)
            LoadGameInSingle();
        else if (StateNameController.gamePlayMode == StateNameController.MULTIPLE_MODE)
            LoadGameInMultiple();
    }

    public void LoadGameInSingle()
    {
        string CurrentInstrSelected = SceneBtnSelected.SelectedInstrName;
        string Selectedlevel = currentLevelSelected.levelSelected;
        string Selectedsong = currentSongSelected.songSelected;

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

    public void LoadGameInMultiple()
    {
        // Determine the room has 4 player
        if (PhotonNetwork.PlayerList.Length == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // Get the song and level selection in the room


            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.ContainsKey(StateNameController.customPropSelectedRole))
                {
                    instrPasstoScene = (string)player.CustomProperties[StateNameController.customPropSelectedRole];
                }
                else
                    Debug.LogError("Player should has the role selected!");
            }
            SceneManager.LoadScene(4);
        }
        else
            Debug.LogError("Player Length should up to 4 people!");
    }
}
