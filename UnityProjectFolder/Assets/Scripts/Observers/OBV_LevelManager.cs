using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OBV_LevelManager : MonoBehaviour
{
    public static OBV_LevelManager instance;

    public int level = -1;
    public float totalGameTime = 0f;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        level += 1;

        switch (level)
        {
            case 0:
                break;
            case 1:
                totalGameTime = 30f;
                break;
            case 2:
                totalGameTime = 40f;
                break;
            case 3:
                totalGameTime = 50f;
                break;
            case 4:
                totalGameTime = 60f;
                break;
            case 5:
                totalGameTime = 70f;
                break;
            case 6:
                totalGameTime = 80f;
                break;
            case 7:
                totalGameTime = 90f;
                break;
            case 8:
                totalGameTime = 100f;
                break;
            case 9:
                totalGameTime = 110f;
                break;
        }

        SceneManager.LoadScene("Orbital_Level" + level);
    }

    public void Restart()
    {
        level = 0;
        ChangeLevel();
    }
}
