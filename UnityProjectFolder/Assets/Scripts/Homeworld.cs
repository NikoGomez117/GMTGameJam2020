using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homeworld : SubscribingMonoBehaviour
{
    public static Homeworld instance;

    public delegate void HealthChanged(float val);
    public static HealthChanged healthChanged;

    private int _health = 3;
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

    void Awake()
    {
        instance = this;
    }

    protected override void Subscribe()
    {
        AlienSpaceship.alienInvaded += AlienInvadedEvent;
    }

    protected override void UnSubscribe()
    {
        AlienSpaceship.alienInvaded -= AlienInvadedEvent;
    }

    public void AlienInvadedEvent()
    {
        _health -= 1;
    }
}
