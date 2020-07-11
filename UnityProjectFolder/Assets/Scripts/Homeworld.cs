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

    public delegate void OnScrapChanged(float val);
    public static OnScrapChanged scrapChanged;

    private int _scrap = 0;
    int Scrap
    {
        get
        {
            return _scrap;
        }
        set
        {
            _scrap = value;
            scrapChanged?.Invoke(_scrap);
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

    public void AlienInvadedEvent(AlienSpaceship alienShip)
    {
        _health -= 1;
    }
}
