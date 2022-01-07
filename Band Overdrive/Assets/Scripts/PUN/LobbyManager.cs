using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Dropdown m_Dropdown; 

    private List<string> m_RoomList = new List<string> { "Room A", "Room B", "Room C", "Room D" };

    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    // Start is called before the first frame update
    void Start()
    {
        initRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initRooms()
    {
        // initialize dropdown options
        m_Dropdown.ClearOptions();
        m_Dropdown.AddOptions(m_RoomList);
    }
    
    public void joinRoom()
    {
        // Get the dropdown selected
        string selected = m_Dropdown.options[m_Dropdown.value].text;
        //StateNameController.seletedRoomName = selected;

        // Set up room custom properties
        Photon.Realtime.RoomOptions roomOptions = new Photon.Realtime.RoomOptions();
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        roomOptions.CustomRoomProperties.Add(StateNameController.roomCustomPropRoom, selected);
        roomOptions.CustomRoomProperties.Add(StateNameController.roomCustomPropSong, null);
        roomOptions.CustomRoomProperties.Add(StateNameController.roomCustomPropLevel, null);
        roomOptions.CustomRoomProperties.Add(StateNameController.roomCustomPropIsStart, false);
        roomOptions.MaxPlayers = maxPlayersPerRoom;

        // Join or create the room
        PhotonNetwork.JoinOrCreateRoom(selected, roomOptions, null);
    }

    public override void OnJoinedRoom()
    {
        // Enter to the multiplayer rest room (for role selection)
        PhotonNetwork.LoadLevel(8);
    }

    public void addRoom(string room)
    {
        m_RoomList.Add(room);
    }

}
