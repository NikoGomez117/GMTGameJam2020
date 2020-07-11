using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienSpaceship : MonoBehaviour
{
    public delegate void HealthChanged(float val);
    public static HealthChanged healthChanged;

    private int _health = 2;
    int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health);
        }
    }

    float speed = 0.1f;
    float spawnTime;
    float trueDistance;

    public delegate void OnAlienInvaded();
    public static OnAlienInvaded alienInvaded;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        CheckInvasion();
    }

    void Spawn()
    {
        spawnTime = Time.time;
        InitPosition();
    }

    void InitPosition()
    {
        transform.position = Vector2.right * 5f;
        transform.RotateAround(Vector3.zero,Vector3.forward,Random.Range(0,6) * 60f);
    }

    void UpdatePosition()
    {
        trueDistance = (5 - speed * (Time.time - spawnTime));
        transform.position = transform.position.normalized * trueDistance;
    }

    void CheckInvasion()
    {
        if (trueDistance <= 0.1f)
        {
            alienInvaded?.Invoke();
            Destroy(gameObject);
        }
    }
}
