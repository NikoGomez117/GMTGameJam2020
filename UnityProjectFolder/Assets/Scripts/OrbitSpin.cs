using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSpin : MonoBehaviour
{
    [SerializeField]
    float spin = 0f;

    private void Update()
    {
        AddSpin();
    }

    void AddSpin()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * spin);
    }
}
