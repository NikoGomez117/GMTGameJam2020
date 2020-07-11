using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTurrent : MonoBehaviour
{
    [SerializeField]
    float dis = 2.5f;

    float orbitSpeed = 180f;

    float rotVel = 0f;
    float orbitTime = 0f;

    float rot = 0f;
    float targetRot = 0f;

    Vector2 prvPos;
    Vector2 nextPos;

    private void OnEnable()
    {
        prvPos = transform.position.normalized * dis;
        nextPos = transform.position.normalized * dis;
    }

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
        transform.position = Vector3.RotateTowards(prvPos, nextPos, rot * Mathf.Deg2Rad, dis);
        transform.right = transform.position.normalized;
    }

    public void OnTarget(Vector2 pos)
    {
        rot = 0f;

        prvPos = transform.position.normalized * dis;
        nextPos = pos.normalized * dis;

        targetRot = Vector2.Angle(prvPos, nextPos);

        orbitTime = targetRot / orbitSpeed;
    }

    // Shooting
}
