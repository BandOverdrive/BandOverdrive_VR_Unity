using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RestRoomManager : MonoBehaviourPunCallbacks
{
    public Text m_RoomName;
    public Text m_ReadyStartState;
    public string m_RoleSelected = null;
    public Text m_PlayerDrum;
    public Text m_PlayerGuitar;
    public Text m_PlayerKeyboard;
    public Text m_PlayerVocal;
    public bool m_IsReady = false;
    public bool m_IsAllReady = false;
    // Role Button
    public Button buttonDrum;
    public Button buttonGuitar;
    public Button buttonKeyboard;
    public Button buttonVocal;
    // Canvas
    public GameObject canvas_SongSelection;
    public GameObject canvas_RoleSelection;
    public CanvasManager m_CanvasManager;
    

    // Start is called before the first frame update
    void Start()
    {
        // Load room custom properties
        m_RoomName.text = PhotonNetwork.CurrentRoom.CustomProperties[StateNameController.roomCustomPropRoom].ToString();

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
        if (PhotonNetwork.CurrentRoom.PlayerCount > 0)
        {
            m_IsAllReady = true;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                //print(player.CustomProperties[StateNameController.customPropIsReady]);
                if (player.CustomProperties.ContainsKey(StateNameController.customPropIsReady))
                {
                    bool _currReady = (bool)player.CustomProperties[StateNameController.customPropIsReady];
                    if (!_currReady)
                    {
                        m_IsAllReady = false;
                        break;
                    }
                }
            }
            // If all 4 player is ready, then countdown to start the game for each
            if (m_IsAllReady && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                // Set Master Client to be the Song Selector
                print("------------- Enter game after 10s --------------");
                print("-- " + PhotonNetwork.IsMasterClient);
                if (PhotonNetwork.CurrentRoom.MasterClientId != -1 && PhotonNetwork.IsMasterClient)
                {
                    string _currSelectedRole = (string)PhotonNetwork.LocalPlayer.CustomProperties[StateNameController.customPropSelectedRole];
                    
                    if (string.IsNullOrEmpty(_currSelectedRole))
                        Debug.LogError("Must has the role selection for the game start!");
                    else
                    {
                        // Display song selection canvas
                        UpdateCanvas(false, true);
                    }
                }

                // Reset the control state
                var hashtable = PhotonNetwork.LocalPlayer.CustomProperties;
                hashtable[StateNameController.customPropIsReady] = false;
                PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
                m_IsAllReady = false;
            }

        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(StateNameController.roomCustomPropIsStart))
        {
            var _isStart = (bool)propertiesThatChanged[StateNameController.roomCustomPropIsStart];
            if (_isStart)
            {
                // Start game base on the player's selected role
                StartGameScene();
            }
        }
        
    }

    public void StartGameScene()
    {
        // Should be four in the future
        if (photonView.IsMine)
        {
            var player = PhotonNetwork.LocalPlayer;
            if (player.CustomProperties.ContainsKey(StateNameController.customPropSelectedRole))
            {
                string _selectedRole = (string)player.CustomProperties[StateNameController.customPropSelectedRole];
                if (!string.IsNullOrEmpty(_selectedRole))
                {
                    switch (_selectedRole)
                    {
                        case "Drum":
                            PhotonNetwork.LoadLevel(1);
                            break;
                        case "Guitar":
                            PhotonNetwork.LoadLevel(2);
                            break;
                        case "Keyboard":
                            PhotonNetwork.LoadLevel(3);
                            break;
                        case "Vocal":
                            PhotonNetwork.LoadLevel(4);
                            break;
                        default: break;
                    }
                }
                else
                    Debug.LogError("player role should be selected!");
            }
            else
                Debug.LogError("properties should contain selected_role!");
        }
        //if (PhotonNetwork.PlayerList.Length > 0)
        //{
        //    foreach (var player in PhotonNetwork.PlayerList)
        //    {
        //        if (player.CustomProperties.ContainsKey(StateNameController.customPropSelectedRole))
        //        {

        //            string _selectedRole = (string)player.CustomProperties[StateNameController.customPropSelectedRole];
        //            if (!string.IsNullOrEmpty(_selectedRole))
        //            {
        //                switch (_selectedRole)
        //                {
        //                    case "Drum":
        //                        PhotonNetwork.LoadLevel(1);
        //                        break;
        //                    case "Guitar":
        //                        PhotonNetwork.LoadLevel(2);
        //                        break;
        //                    case "Keyboard":
        //                        PhotonNetwork.LoadLevel(3);
        //                        break;
        //                    case "Vocal":
        //                        PhotonNetwork.LoadLevel(4);
        //                        break;
        //                    default: break;
        //                }
        //            }
        //            else
        //                Debug.LogError("player role should be selected!");
        //        }
        //        else
        //            Debug.LogError("properties should contain selected_role!");
        //    }
        //}
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
            string fromPlayerName = PhotonNetwork.LocalPlayer.NickName;
            //print("---------------- " + fromPlayerName);
            //print("================ " + PlayerPrefs.GetString(StateNameController.playerNamePrefKey));
            photonView.RPC("ChangeRoleSelected", RpcTarget.AllBuffered, fromPlayerName, m_RoleSelected, m_IsReady);

            bool _isEnable = !m_IsReady;
            photonView.RPC("SetButtonInteractable", RpcTarget.AllBuffered, m_RoleSelected, _isEnable);
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

    public void OnPlayButtonClick()
    {
        // Check for the song and level selection
        if (string.IsNullOrEmpty(m_CanvasManager.m_SongSelected) || string.IsNullOrEmpty(m_CanvasManager.m_LevelSelected))
        {
            Debug.LogError("Song selection must not include empty option");
            return;
        }

        var hashtable = PhotonNetwork.CurrentRoom.CustomProperties;
        hashtable[StateNameController.roomCustomPropSong] = m_CanvasManager.m_SongSelected;
        hashtable[StateNameController.roomCustomPropLevel] = m_CanvasManager.m_LevelSelected;
        hashtable[StateNameController.roomCustomPropIsStart] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }

    [PunRPC]
    void ChangeRoleSelected(string fromPlayer, string roleSelected, bool isReady)
    {
        switch (roleSelected)
        {
            case "Drum":
                //PhotonNetwork.CurrentRoom.CustomProperties["role_drum"] = true;
                m_PlayerDrum.text = isReady ? (fromPlayer + " Ready") : "Waiting..";
                break;
            case "Guitar":
                m_PlayerGuitar.text = isReady ? (fromPlayer + " Ready") : "Waiting..";
                break;
            case "Keyboard":
                m_PlayerKeyboard.text = isReady ? (fromPlayer + " Ready") : "Waiting..";
                break;
            case "Vocal":
                m_PlayerVocal.text = isReady ? (fromPlayer + " Ready") : "Waiting..";
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

    [PunRPC]
    void UpdateCanvas(bool roleSelectionCanvasEnable, bool songSelectionCanvasEnable)
    {
        canvas_RoleSelection.SetActive(roleSelectionCanvasEnable);
        canvas_SongSelection.SetActive(songSelectionCanvasEnable);
    }

}
