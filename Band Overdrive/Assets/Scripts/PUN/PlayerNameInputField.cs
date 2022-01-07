using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(Text))]
public class PlayerNameInputField : MonoBehaviour
{
    private string m_CurrentPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;
        Text _input = GetComponent<Text>();
        print("----------- " + _input + " ----------- ");
        if (_input != null)
        {
            if (PlayerPrefs.HasKey(StateNameController.playerNamePrefKey))
            {
                //PlayerPrefs.SetString(StateNameController.playerNamePrefKey, "Penny");
                defaultName = PlayerPrefs.GetString(StateNameController.playerNamePrefKey);
                _input.text = defaultName;
            }
            else
            {
                defaultName = GetRandomPlayerName();
                SetPlayerName(defaultName);
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("Player name is null or empty!");
            return;
        }
        PhotonNetwork.NickName = name;

        PlayerPrefs.SetString(StateNameController.playerNamePrefKey, name);
    }

    public string GetRandomPlayerName()
    {
        string name = string.Empty;
        int index = 0;
        List<StateNameController.PlayerNameKeyValue> list = StateNameController.playerNameList;
        foreach(var item in list)
        {
            if (!item.isUsed)
            {
                // Name can be used
                name = item.name;
                var playerName = StateNameController.playerNameList[index];
                playerName.isUsed = true;
                StateNameController.playerNameList[index] = playerName;
                break;
            }
            index++;
        }

        return name;
    }
}
