using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBV_CameraManager : Observer
{
    public AnimationCurve shakeCurve;

    IEnumerator shakeRoutine;

    float shakeFrequency = 0.1f;

    Vector3 initPos;

    float smoothTime = 0.2f;
    Vector3 smoothVel = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        initPos = transform.position;
    }

    protected override void Subscribe()
    {
        ACT_OrbitalTurrent.turretFired += TurretFiredShake;
    }

    protected override void UnSubscribe()
    {
        ACT_OrbitalTurrent.turretFired -= TurretFiredShake;
    }

    void TurretFiredShake()
    {
        ShakeScreen(0.5f,0.5f);
    }

    private void Update()
    {
        SmoothReturn();
    }

    void SmoothReturn()
    {
        transform.position = Vector3.SmoothDamp(transform.position, initPos, ref smoothVel, smoothTime);
    }

    public void ShakeScreen(float magnitude, float duration)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = AnimateShake(magnitude, duration);
        StartCoroutine(shakeRoutine);
    }

    IEnumerator AnimateShake(float magnitude, float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            float delta = 1f - (endTime - Time.time);

            transform.position = transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * magnitude * shakeCurve.Evaluate(delta);
            yield return new WaitForSeconds(shakeFrequency);
        }
    }
}
