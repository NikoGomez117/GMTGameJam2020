using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller instance;

    public Sprite[] numSprites;

    private void Awake()
    {
        instance = this;
    }
}
