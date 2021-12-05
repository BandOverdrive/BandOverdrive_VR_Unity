using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Track track;
    public GameObject gameScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameScore.GetComponent<Text>().text =  ((int)(10000 * currentScore.m_Accuracy)).ToString();
        gameScore.GetComponent<Text>().text = ((int)( track.m_CurrentScore)).ToString();
    }
}
