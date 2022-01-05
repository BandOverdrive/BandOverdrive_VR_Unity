using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RestRoomManager : MonoBehaviourPun
{
    public Text m_RoomName;
    public Text m_ReadyStartState;
    public string m_RoleSelected;
    public Text m_PlayerDrum;
    public Text m_PlayerGuitar;
    public Text m_PlayerKeyboard;
    public Text m_PlayerVocal;
    public bool m_IsReady = false;


    // Start is called before the first frame update
    void Start()
    {
        // Load room custom properties
        m_RoomName.text = PhotonNetwork.CurrentRoom.CustomProperties["room_name"].ToString();
        // Add room custom properties;
        //PhotonNetwork.CurrentRoom.CustomProperties.Add("role_drum", false);
        //PhotonNetwork.CurrentRoom.CustomProperties.Add("role_guitar", false);
        //PhotonNetwork.CurrentRoom.CustomProperties.Add("role_keyboard", false);
        //PhotonNetwork.CurrentRoom.CustomProperties.Add("role_vocal", false);

        //m_ReadyStartState.text = PhotonNetwork.CurrentRoom.PlayerCount == 1 ? "Start" : "Ready";
        m_ReadyStartState.text = "Ready";
    }

    // Update is called once per frame
    void Update()
    {
        // Update the current ready state for all players
        //m_PlayerDrum.text = (bool)PhotonNetwork.CurrentRoom.CustomProperties["role_drum"] ? "Ready" : "Waiting..";
        //m_PlayerGuitar.text = (bool)PhotonNetwork.CurrentRoom.CustomProperties["role_guitar"] ? "Ready" : "Waiting..";
        //m_PlayerKeyboard.text = (bool)PhotonNetwork.CurrentRoom.CustomProperties["role_keyboard"] ? "Ready" : "Waiting..";
        //m_PlayerVocal.text = (bool)PhotonNetwork.CurrentRoom.CustomProperties["role_vocal"] ? "Ready" : "Waiting..";


        // If all 4 player is ready, then countdown to start the game for each
    }

    public void ReadyOrStartClick()
    {
        if (m_ReadyStartState.text == "Ready")
        {
            //PhotonNetwork.LocalPlayer.CustomProperties.Add("player_role", m_RoleSelected);
            //PhotonNetwork.CurrentRoom.CustomProperties.Add("role_drum", true);
            print(photonView);
            photonView.RPC("ChangeRoleSelected", RpcTarget.All, m_RoleSelected);
            //switch (m_RoleSelected)
            //{
            //    case "Drum":
                    
            //        break;
            //    case "Guitar":
                    
            //        break;
            //    case "Keyboard":
                    
            //        break;
            //    case "Vocal":
                    
            //        break;
            //    default: break;
            //}

            m_IsReady = true;
            m_ReadyStartState.text = "Cancel";
        }
        else
        {
            //switch (m_RoleSelected)
            //{
            //    case "Drum":
            //        m_PlayerDrum.text = "Waiting...";
            //        break;
            //    case "Guitar":
            //        m_PlayerGuitar.text = "Waiting...";
            //        break;
            //    case "Keyboard":
            //        m_PlayerKeyboard.text = "Waiting...";
            //        break;
            //    case "Vocal":
            //        m_PlayerVocal.text = "Waiting...";
            //        break;
            //    default: break;
            //}

            m_IsReady =false;
            m_ReadyStartState.text = "Ready";
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
    void ChangeRoleSelected(string roleSelected)
    {
        switch (roleSelected)
        {
            case "Drum":
                //PhotonNetwork.CurrentRoom.CustomProperties["role_drum"] = true;
                m_PlayerDrum.text = "Ready";
                break;
            case "Guitar":
                m_PlayerGuitar.text = "Ready";
                break;
            case "Keyboard":
                m_PlayerKeyboard.text = "Ready";
                break;
            case "Vocal":
                m_PlayerVocal.text = "Ready";
                break;
            default: break;
        }

    }
}
