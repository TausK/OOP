using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HundredBalls
    {
public class GameManager : MonoBehaviour {

    public static int score;
    public int goal = 0;

    void Start()
    {
        score = 0;
        goal = 100;
        Time.timeScale = 2;
    }

    void Update()
    {
        if (score > goal)
        {
            Time.timeScale = 10;
        }
    }

    void lateUpdate()
    {
        if (score == goal)
        {
            goal++;
        }
    }

    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        GUI.Label(new Rect(scrW * 0.25f, scrH * 0.25f, scrW * 2f, scrH * 0.5f), "Score: " + score);
        GUI.Label(new Rect(scrW * 0.25f, scrH * 1f, scrW * 2f, scrH * 0.5f), "Goal: " + goal + " Points");

    }
}
}