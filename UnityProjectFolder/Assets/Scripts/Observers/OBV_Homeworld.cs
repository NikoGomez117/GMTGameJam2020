using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OBV_Homeworld : Observer
{
    [SerializeField]
    AudioSource resupplySound;

    float sceneStartTime;

    float ammoResupply = 2.5f;
    GameObject prvTurret;

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

    private void Update()
    {
        Resupply();
        CheckWarp();
    }

    void Resupply()
    {

        if (ACT_InputManager.selectedObj != prvTurret)
        {
            resupplySound.Stop();
        }

        if (ACT_InputManager.selectedObj != null && ACT_InputManager.selectedObj.CompareTag("OrbitalTurret"))
        {
            if (ACT_InputManager.selectedObj != prvTurret)
            {
                prvTurret = ACT_InputManager.selectedObj;
            }

            if (!resupplySound.isPlaying && prvTurret.GetComponent<ACT_OrbitalTurrent>().Ammo < 6 && myStats.ScrapRemainder + myStats.Scrap > 0)
            {
                resupplySound.time = 0f;
                resupplySound.Play();

                GetComponent<LineRenderer>().enabled = true;
            }
            else if (prvTurret.GetComponent<ACT_OrbitalTurrent>().Ammo >= 6 || myStats.ScrapRemainder + myStats.Scrap <= 0)
            {
                resupplySound.Stop();

                GetComponent<LineRenderer>().enabled = false;
            }
            else if(resupplySound.isPlaying)
            {
                GetComponent<LineRenderer>().SetPositions( new Vector3[] { 
                    transform.position,
                    prvTurret.transform.position
                });

                prvTurret.GetComponent<ACT_OrbitalTurrent>().Ammo += Time.deltaTime * ammoResupply;
                myStats.ScrapRemainder -= Time.deltaTime * ammoResupply / 4f;

                if (myStats.ScrapRemainder < 0)
                {
                    if (myStats.Scrap > 0)
                    {
                        myStats.Scrap -= 1;
                        myStats.ScrapRemainder += 1f;
                    }
                    else
                    {
                        myStats.ScrapRemainder = 0f;
                    }
                }
            }
        }
    }

    void CheckWarp()
    {
        if (Time.time - sceneStartTime >= OBV_LevelManager.instance.totalGameTime)
        {
            OBV_LevelManager.instance.ChangeLevel();
        }
    }
}
