using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RestRoomManager : MonoBehaviourPun
{
    public Text m_RoomName;
    public Text m_ReadyStartState;
    public string m_RoleSelected = null;
    public Text m_PlayerDrum;
    public Text m_PlayerGuitar;
    public Text m_PlayerKeyboard;
    public Text m_PlayerVocal;
    public bool m_IsReady = false;
    public bool m_IsGameReady = false;
    // Role Button
    public Button buttonDrum;
    public Button buttonGuitar;
    public Button buttonKeyboard;
    public Button buttonVocal;


    // Start is called before the first frame update
    void Start()
    {
        // Load room custom properties
        m_RoomName.text = PhotonNetwork.CurrentRoom.CustomProperties["room_name"].ToString();

        // Set up current player state
        string _playerName = PlayerPrefs.GetString(StateNameController.playerNamePrefKey);
        if (!string.IsNullOrEmpty(_playerName))
        {
            // Set NickName
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString(StateNameController.playerNamePrefKey);
            // Set custom properties
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add(StateNameController.customPropIsReady, m_IsReady);
            hashtable.Add(StateNameController.customPropSelectedRole, m_RoleSelected);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
        else
            Debug.LogError("Player name is null or empty!");

        // Set up the initialize state for the room
        m_ReadyStartState.text = "Ready";
    }

    // Update is called once per frame
    void Update()
    {
        // Update the current interactable for each role button
        if (PhotonNetwork.CountOfPlayersInRooms > 0)
        {
            m_IsGameReady = true;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                bool _currReady = (bool)player.CustomProperties[StateNameController.customPropIsReady];
                string _currSelectedRole = (string)player.CustomProperties[StateNameController.customPropSelectedRole];
                if (!_currReady)
                {
                    m_IsGameReady = false;
                    break;
                }
            }
            // If all 4 player is ready, then countdown to start the game for each
            if (m_IsGameReady)
            {
                // Enter game after 10 seconds
                print("------------- Enter game after 10s --------------");
            }
        }
    }

    public void ReadyOrStartClick()
    {
        //print(" ------ + " + m_RoleSelected + " +--------");
        if (m_IsReady == false)
        {
            if (m_RoleSelected.Length > 0)
            {
                m_IsReady = true;
                m_ReadyStartState.text = "Cancel";
            }
            // Add tips for unselected ready
        }
        else
        {
            if (m_RoleSelected.Length > 0)
            {
                m_IsReady = false;
                m_ReadyStartState.text = "Ready";
            }
        }

        // Update the currentPlayer status and currentRoom setting
        var hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
        hashtable[StateNameController.customPropIsReady] = m_IsReady;
        hashtable[StateNameController.customPropSelectedRole] = m_IsReady ? m_RoleSelected : null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);

        // RPC update
        // !!To test the multiplayer in one client, set the photonView or set the photonView.IsMine
        if (photonView)
        {
            photonView.RPC("ChangeRoleSelected", RpcTarget.All, m_RoleSelected, m_IsReady);

            bool _isEnable = !m_IsReady;
            photonView.RPC("SetButtonInteractable", RpcTarget.All, m_RoleSelected, _isEnable);
        }

    }

    public void onDrumSelected()
    {
        if (!m_IsReady)
            m_RoleSelected = "Drum";
    }
    public void onGuitarSelected()
    {
        if (!m_IsReady)
            m_RoleSelected = "Guitar";
    }
    public void onKeyboardSelected()
    {
        if (!m_IsReady)
            m_RoleSelected = "Keyboard";
    }
    public void onVocalSelected()
    {
        if (!m_IsReady)
            m_RoleSelected = "Vocal";
    }

    [PunRPC]
    void ChangeRoleSelected(string roleSelected, bool isReady)
    {
        switch (roleSelected)
        {
            case "Drum":
                //PhotonNetwork.CurrentRoom.CustomProperties["role_drum"] = true;
                m_PlayerDrum.text = isReady ? (PhotonNetwork.NickName + " Ready") : "Waiting..";
                break;
            case "Guitar":
                m_PlayerGuitar.text = isReady ? (PhotonNetwork.NickName + " Ready") : "Waiting..";
                break;
            case "Keyboard":
                m_PlayerKeyboard.text = isReady ? (PhotonNetwork.NickName + " Ready") : "Waiting..";
                break;
            case "Vocal":
                m_PlayerVocal.text = isReady ? (PhotonNetwork.NickName + " Ready") : "Waiting..";
                break;
            default: break;
        }

    }

    [PunRPC]
    void SetButtonInteractable(string roleSelected, bool isEnable)
    {
        switch (roleSelected)
        {
            case "Drum":
                buttonDrum.interactable = isEnable;
                break;
            case "Guitar":
                buttonGuitar.interactable = isEnable;
                break;
            case "Keyboard":
                buttonKeyboard.interactable = isEnable;
                break;
            case "Vocal":
                buttonVocal.interactable = isEnable;
                break;
            default: break;
        }
    }

}
