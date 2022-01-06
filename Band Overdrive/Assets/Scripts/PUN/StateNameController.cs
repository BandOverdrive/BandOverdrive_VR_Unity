using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static string seletedRoomName;

    public const string playerNamePrefKey = "player_name";
    public const string customPropIsReady = "is_ready";
    public const string customPropSelectedRole = "selected_role";

    public struct PlayerNameKeyValue
    {
        public string name;
        public bool isUsed;

        public PlayerNameKeyValue(string name, bool isUsed)
        {
            this.name = name;
            this.isUsed = isUsed;
        }

        
    };

    public static List<PlayerNameKeyValue> playerNameList = new List<PlayerNameKeyValue>
    {
        new PlayerNameKeyValue("Tom", false),
        new PlayerNameKeyValue("Penny", false),
        new PlayerNameKeyValue("John", false),
        new PlayerNameKeyValue("Homie", false),
        new PlayerNameKeyValue("Kate", false),
        new PlayerNameKeyValue("Tim", false),
        new PlayerNameKeyValue("Rose", false),
        new PlayerNameKeyValue("Jack", false),
        
    };
    
}
