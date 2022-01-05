using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RestRoomManager : MonoBehaviour
{
    public Text m_RoomName;

    // Start is called before the first frame update
    void Start()
    {
        m_RoomName.text = PhotonNetwork.CurrentRoom.CustomProperties["room_name"].ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
