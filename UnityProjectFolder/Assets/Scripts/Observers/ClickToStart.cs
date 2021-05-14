using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : Observer
{
    protected override void Subscribe()
    {
        ACT_InputManager.emptySelection += NextLevel;
        ACT_InputManager.emptyTarget += NextLevel;
    }

    protected override void UnSubscribe()
    {
        ACT_InputManager.emptySelection -= NextLevel;
        ACT_InputManager.emptyTarget -= NextLevel;
    }

    void NextLevel()
    {
        OBV_LevelManager.instance.ChangeLevel();
    }
}
