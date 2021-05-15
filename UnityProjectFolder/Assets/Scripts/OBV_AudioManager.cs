using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBV_AudioManager : Observer
{
    float masterVolume = 0.1f;
    AudioSource myAudio;

    protected override void Awake()
    {
        base.Awake();
        myAudio = GetComponent<AudioSource>();
    }

    protected override void Subscribe()
    {
        // Stuff
    }

    protected override void UnSubscribe()
    {
        // Stuff
    }
}
