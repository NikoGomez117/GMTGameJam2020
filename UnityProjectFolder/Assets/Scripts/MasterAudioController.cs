using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAudioController : MonoBehaviour
{
    private void Start()
    {
        foreach (AudioSource AS in GetComponentsInChildren<AudioSource>(true))
        {
            AS.volume *= 0.1f;
        }
    }
}
