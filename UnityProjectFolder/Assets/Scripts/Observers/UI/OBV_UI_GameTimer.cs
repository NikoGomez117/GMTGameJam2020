using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OBV_UI_GameTimer : MonoBehaviour
{
    float levelStartTime;

    // Start is called before the first frame update
    void Start()
    {
        levelStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        SetTimer();
        CheckWarp();
    }

    void SetTimer()
    {
        TimeSpan ts = TimeSpan.FromSeconds(OBV_LevelManager.instance.totalGameTime - (Time.time - levelStartTime));
        GetComponent<TextMeshProUGUI>().text = "JUMP IN" + "\n " + ts.ToString("m':'ss");
    }

    void CheckWarp()
    {
        if (Time.time - levelStartTime >= OBV_LevelManager.instance.totalGameTime)
        {
            OBV_LevelManager.instance.ChangeLevel();
        }
    }
}
