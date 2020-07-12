using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Homeworld_Health : SubscribingMonoBehaviour
{
    [SerializeField]
    Image myRenderer;
    [SerializeField]
    Animator myAnim;

    protected override void Subscribe()
    {
        Homeworld.healthChanged += HealthChangedEvent;
    }
    protected override void UnSubscribe()
    {
        Homeworld.healthChanged -= HealthChangedEvent;
    }

    private void HealthChangedEvent(float delta)
    {
        myRenderer.sprite = UI_Controller.instance.numSprites[Homeworld.instance.Health];

        if (delta < 0)
        {
            myAnim.Play("LoseHealth");
        }
    }
}
