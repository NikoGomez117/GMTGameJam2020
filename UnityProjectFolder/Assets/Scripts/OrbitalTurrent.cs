using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTurrent : SubscribingMonoBehaviour
{
    float dis = 2.5f;
    float rot = 0f;
    float orbitSpeed = 60f;

    float targetRot = 0f;
    float rotVel = 0f;
    float orbitTime = 0f;

    // Selection / Movement
    private void Update()
    {
        UpdateMovement();
        Reposition();
    }

    void UpdateMovement()
    {
        rot = Mathf.SmoothDamp(rot, targetRot, ref rotVel, orbitTime);
    }

    void Reposition()
    {
        transform.position = Vector2.right * dis;
        transform.RotateAround(Vector3.zero, Vector3.forward, rot);
    }

    public void OnTarget(Vector2 pos)
    {
        targetRot = Vector2.Angle(Vector2.right,pos);
        orbitTime = Mathf.Abs(targetRot - rot) / orbitSpeed;
    }

    // Shooting
}
