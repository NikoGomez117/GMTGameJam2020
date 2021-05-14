using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OBV_UI_Homeworld_Health : Observer
{
    [SerializeField]
    Image myRenderer;
    [SerializeField]
    Animator myAnim;

    protected override void Subscribe()
    {
        ACT_HomeworldStats.healthChanged += HealthChangedEvent;
    }
    protected override void UnSubscribe()
    {
        ACT_HomeworldStats.healthChanged -= HealthChangedEvent;
    }

    private void HealthChangedEvent(float delta)
    {
        myRenderer.sprite = OBV_UI_Controller.instance.numSprites[((OBV_Homeworld)OBV_Homeworld.instance).GetHealth()];

        if (delta < 0)
        {
            myAnim.Play("LoseHealth");
        }
    }
}
