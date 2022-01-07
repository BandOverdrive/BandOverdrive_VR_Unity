using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Track;
using Photon.Pun;

public class CurrentSceneOption : MonoBehaviour
{
    public Track currentTrack;
    public Track currentSongPath;
    public string currentSceneLevel;
    public string currentSceneSong;

    // Start is called before the first frame update
    void Start()
    {
        if (StateNameController.gamePlayMode == StateNameController.SINGLE_MODE)
        {
            currentSceneLevel = SceneLoadManager.levelPasstoScene;
            currentSceneSong = SceneLoadManager.songPasstoScene;
        }
        else if (StateNameController.gamePlayMode == StateNameController.MULTIPLE_MODE)
        {
            // Load selected level
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(StateNameController.roomCustomPropLevel))
                currentSceneLevel = (string)PhotonNetwork.CurrentRoom.CustomProperties[StateNameController.roomCustomPropLevel];
            else
                Debug.LogError("Should has the level custom prop key!");

            // Load selected song
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(StateNameController.roomCustomPropSong))
                currentSceneSong = (string)PhotonNetwork.CurrentRoom.CustomProperties[StateNameController.roomCustomPropSong];
            else
                Debug.LogError("Should has the song custom prop key!");
        }
        
        // Set up the song path and level
        currentSongPath.m_SongAssetsPath = currentSceneSong;

        if (currentSceneLevel == "Easy")
        {
            currentTrack.m_CurrentLevel = Level.Easy;
        }
        else if (currentSceneLevel == "Medium")
        {
            currentTrack.m_CurrentLevel = Level.Medium;
        }
        else if (currentSceneLevel == "Hard")
        {
            currentTrack.m_CurrentLevel = Level.Hard;
        }
        else if (currentSceneLevel == "Expert")
        {
            currentTrack.m_CurrentLevel = Level.Expert;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
