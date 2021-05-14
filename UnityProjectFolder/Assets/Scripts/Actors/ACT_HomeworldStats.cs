using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT_HomeworldStats : Actor
{
    public delegate void OnHomeworldDestroyed();
    public static OnHomeworldDestroyed homeworldDestroyed;

    public delegate void HealthChanged(float delta);
    public static HealthChanged healthChanged;

    private int _health = 1;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            int delta = value - _health;
            _health = value;
            healthChanged?.Invoke(delta);

            if (_health <= 0)
            {
                homeworldDestroyed?.Invoke();
            }
        }
    }

    public delegate void OnScrapChanged(float delta);
    public static OnScrapChanged scrapChanged;

    private int _scrap = 0;
    public int Scrap
    {
        get
        {
            return _scrap;
        }
        set
        {
            int delta = value - _scrap;
            _scrap = value;
            scrapChanged?.Invoke(delta);
        }
    }

    public delegate void OnScrapRemainderChanged(float val);
    public static OnScrapRemainderChanged scrapRemainderChanged;

    private float _scrapRemainder = 0f;
    public float ScrapRemainder
    {
        get
        {
            return _scrapRemainder;
        }
        set
        {
            _scrapRemainder = value;
            scrapRemainderChanged?.Invoke(_scrapRemainder);
        }
    }
}
