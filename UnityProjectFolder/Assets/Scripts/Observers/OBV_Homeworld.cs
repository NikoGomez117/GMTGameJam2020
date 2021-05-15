using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OBV_Homeworld : Observer
{
    float sceneStartTime;

    ACT_HomeworldStats myStats;

    protected override void Awake()
    {
        base.Awake();
        myStats = gameObject.AddComponent<ACT_HomeworldStats>();
    }

    protected override void Start()
    {
        base.Start();

        sceneStartTime = Time.time;
        myStats.Health = 1;
    }

    protected override void Subscribe()
    {
        ACT_AlienSpaceship.alienInvaded += AlienInvadedEvent;
    }

    protected override void UnSubscribe()
    {
        ACT_AlienSpaceship.alienInvaded -= AlienInvadedEvent;
    }

    public void AlienInvadedEvent(ACT_AlienSpaceship alienShip)
    {
        myStats.Health -= 1;
    }
    public void AddHealth(int delta)
    {
        myStats.Health += delta;
    }

    public void SetHealth(int val)
    {
        myStats.Health = val;
    }

    public int GetHealth()
    {
        return myStats.Health;
    }

    public void SetScrap(int val)
    {
        myStats.Scrap = val;
    }

    public void AddScrap(int delta)
    {
        myStats.Scrap += delta;
    }

    public int GetScrap()
    {
        return myStats.Scrap;
    }

    public void SetScrapRemainder(float value)
    {
        myStats.ScrapRemainder = value;
    }

    public void AddScrapRemainder(float delta)
    {
        myStats.ScrapRemainder += delta;
    }

    public float GetScrapRemainder()
    {
        return myStats.ScrapRemainder;
    }
}
