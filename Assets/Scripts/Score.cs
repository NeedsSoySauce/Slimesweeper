using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    Score instance;

    public static int score = 0;
    public static int displayScore = 0;
    TextMeshProUGUI text;
    GameManager gm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine("showScore");
        ResetScore();
    }

    public static void ResetScore()
    {
        score = 0;
        displayScore = 0;
    }

    public static void addScore(int value)
    {
        score += value;
    }

    IEnumerator showScore()
    {
        while (true)
        {
            displayScore = (int)(Mathf.SmoothStep(displayScore, score, 0.5f) + 0.5);
            text.text = "Score: " + displayScore;
            yield return null;
        }
    }

}
