using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBV_UI_Controller : MonoBehaviour
{
    public static OBV_UI_Controller instance;

    public Sprite[] numSprites;

    private void Awake()
    {
        instance = this;
    }
}
