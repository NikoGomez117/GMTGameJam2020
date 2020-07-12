using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlienSpaceship : MonoBehaviour
{
    [SerializeField]
    GameObject damageSprites;

    private int _health = 2;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            // Edit the view here; 

            if (_health == 1)
            {
                damageSprites.SetActive(true);
            }

            if (_health <= 0)
                alienDestroyed?.Invoke(this);
        }
    }

    float speed = 0.2f;
    float spawnTime;
    float trueDistance;

    public delegate void OnAlienDestroyed(AlienSpaceship alienShip);
    public static OnAlienDestroyed alienDestroyed;

    public delegate void OnAlienInvaded(AlienSpaceship alienShip);
    public static OnAlienInvaded alienInvaded;

    // Start is called before the first frame update
    void OnEnable()
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
        transform.position = Vector2.right * 6.5f;
        transform.RotateAround(Vector3.zero,Vector3.forward,Random.Range(0,36) * 10f);
        transform.right = -transform.position.normalized;
    }

    void UpdatePosition()
    {
        trueDistance = (6.5f - speed * (Time.time - spawnTime));
        transform.position = transform.position.normalized * trueDistance;
    }

    void CheckInvasion()
    {
        if (trueDistance <= 0.5f)
        {
            alienInvaded?.Invoke(this);
        }
    }
}
