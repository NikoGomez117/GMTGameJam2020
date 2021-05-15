using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBV_AudioManager : Observer
{
    [SerializeField]
    AudioSource myAudioSource;

    [Serializable]
    public class AudioVolumePair
    {
        public AudioClip clip;
        public float volume;
    }

    [SerializeField]
    AudioVolumePair selectSound;
    [SerializeField]
    AudioVolumePair targetInputSound;
    [SerializeField]
    AudioVolumePair pickupSound;
    [SerializeField]
    AudioVolumePair chargeSound;
    [SerializeField]
    AudioVolumePair fireSound;
    [SerializeField]
    AudioVolumePair resupplySound;
    [SerializeField]
    AudioVolumePair targetOrbitSound;
    
    [SerializeField]
    float masterVolume = 1f;

    protected override void Subscribe()
    {
        ACT_InputManager.selectTurret += PlaySelectSound;
        ACT_InputManager.selectScrap += PlayPickupSound;

        ACT_OrbitalTurrent.startTurretCharging += PlayChargeSound;
        ACT_OrbitalTurrent.turretFired += PlayTurretFireSound;

        ACT_Resupplier.reSupply += PlayResupplySound;
    }

    protected override void UnSubscribe()
    {
        ACT_InputManager.selectTurret -= PlaySelectSound;
        ACT_InputManager.selectScrap -= PlayPickupSound;

        ACT_OrbitalTurrent.startTurretCharging -= PlayChargeSound;
        ACT_OrbitalTurrent.turretFired -= PlayTurretFireSound;

        ACT_Resupplier.reSupply -= PlayResupplySound;
    }

    void PlaySelectSound()
    {
        myAudioSource.PlayOneShot(selectSound.clip, masterVolume * selectSound.volume);
    }

    void PlayPickupSound()
    {
        myAudioSource.PlayOneShot(pickupSound.clip, masterVolume * pickupSound.volume);
    }

    void PlayChargeSound()
    {
        myAudioSource.PlayOneShot(chargeSound.clip, masterVolume * chargeSound.volume);
    }

    void PlayTurretFireSound()
    {
        myAudioSource.PlayOneShot(fireSound.clip, masterVolume * fireSound.volume);
    }

    void PlayResupplySound()
    {
        myAudioSource.PlayOneShot(resupplySound.clip, masterVolume * resupplySound.volume);
    }
}