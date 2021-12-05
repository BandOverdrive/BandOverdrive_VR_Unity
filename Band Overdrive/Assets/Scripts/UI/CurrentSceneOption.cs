using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Track;

public class CurrentSceneOption : MonoBehaviour
{
    public Track currentTrack;
    public Track currentSongPath;
    public string currentSceneLevel;
    public string currentSceneSong;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneLevel = SceneLoadManager.levelPasstoScene;
        currentSceneSong = SceneLoadManager.songPasstoScene;

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
