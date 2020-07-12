using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AnimationCurve shakeCurve;

    IEnumerator shakeRoutine;

    float shakeFrequency = 0.1f;

    Vector3 initPos;

    float smoothTime = 0.2f;
    Vector3 smoothVel = Vector3.zero; 

    private void Start()
    {
        initPos = transform.position;
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

            transform.position = transform.position + UnityEngine.Random.insideUnitSphere * magnitude * shakeCurve.Evaluate(delta);
            yield return new WaitForSeconds(shakeFrequency);
        }
    }
}
