using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Homeworld : SubscribingMonoBehaviour
{
    public static Homeworld instance;

    [SerializeField]
    AudioSource resupplySound;

    float ammoResupply = 2.5f;
    GameObject prvTurret;

    public delegate void HealthChanged(float delta);
    public static HealthChanged healthChanged;

    private int _health = 3;
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    float scrapRemainder = 0f;

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
        Health -= 1;
    }

    private void Update()
    {
        Resupply();
    }

    void Resupply()
    {

        if (InputController.selectedObj != prvTurret)
        {
            resupplySound.Stop();
        }

        if (InputController.selectedObj != null && InputController.selectedObj.CompareTag("OrbitalTurret"))
        {
            if (InputController.selectedObj != prvTurret)
            {
                prvTurret = InputController.selectedObj;
            }

            if (!resupplySound.isPlaying && prvTurret.GetComponent<OrbitalTurrent>().Ammo < 10 && scrapRemainder + Scrap > 0)
            {
                resupplySound.time = 0f;
                resupplySound.Play();
            }
            else if (prvTurret.GetComponent<OrbitalTurrent>().Ammo >= 10 || scrapRemainder + Scrap <= 0)
            {
                resupplySound.Stop();
            }
            else if(resupplySound.isPlaying)
            {
                prvTurret.GetComponent<OrbitalTurrent>().Ammo += Time.deltaTime * ammoResupply;
                scrapRemainder -= Time.deltaTime * ammoResupply / 4f;

                if (scrapRemainder < 0)
                {
                    if (Scrap > 0)
                    {
                        Scrap -= 1;
                        scrapRemainder += 1f;
                    }
                    else
                    {
                        scrapRemainder = 0f;
                    }
                }
            }
        }
    }
}
