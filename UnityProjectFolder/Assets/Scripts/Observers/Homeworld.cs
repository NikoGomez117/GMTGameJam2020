using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Homeworld : SubscribingMonoBehaviour
{
    public static Homeworld instance;

    [SerializeField]
    AudioSource resupplySound;

    float sceneStartTime;

    float ammoResupply = 2.5f;
    GameObject prvTurret;

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

    void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();

        sceneStartTime = Time.time;
        Health = 1;
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
        CheckWarp();
    }

    void Resupply()
    {

        if (OBV_InputManager.selectedObj != prvTurret)
        {
            resupplySound.Stop();
        }

        if (OBV_InputManager.selectedObj != null && OBV_InputManager.selectedObj.CompareTag("OrbitalTurret"))
        {
            if (OBV_InputManager.selectedObj != prvTurret)
            {
                prvTurret = OBV_InputManager.selectedObj;
            }

            if (!resupplySound.isPlaying && prvTurret.GetComponent<OrbitalTurrent>().Ammo < 6 && ScrapRemainder + Scrap > 0)
            {
                resupplySound.time = 0f;
                resupplySound.Play();

                GetComponent<LineRenderer>().enabled = true;
            }
            else if (prvTurret.GetComponent<OrbitalTurrent>().Ammo >= 6 || ScrapRemainder + Scrap <= 0)
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

                prvTurret.GetComponent<OrbitalTurrent>().Ammo += Time.deltaTime * ammoResupply;
                ScrapRemainder -= Time.deltaTime * ammoResupply / 4f;

                if (ScrapRemainder < 0)
                {
                    if (Scrap > 0)
                    {
                        Scrap -= 1;
                        ScrapRemainder += 1f;
                    }
                    else
                    {
                        ScrapRemainder = 0f;
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
