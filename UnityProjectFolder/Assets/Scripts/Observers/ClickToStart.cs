using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : SubscribingMonoBehaviour
{
    protected override void Subscribe()
    {
        OBV_InputManager.emptySelection += NextLevel;
        OBV_InputManager.emptyTarget += NextLevel;
    }

    protected override void UnSubscribe()
    {
        OBV_InputManager.emptySelection -= NextLevel;
        OBV_InputManager.emptyTarget -= NextLevel;
    }

    void NextLevel()
    {
        OBV_LevelManager.instance.ChangeLevel();
    }
}
