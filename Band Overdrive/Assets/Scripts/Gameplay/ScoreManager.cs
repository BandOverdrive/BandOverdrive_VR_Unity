using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshPro scoreText;
    static int comboScore;

    void Start()
    {
        Instance = this;
        comboScore = 0;
    }

    public static void Hit()
    {
        comboScore += 1;
    }

    public static void Miss()
    {
        comboScore = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = comboScore.ToString();
    }
}
