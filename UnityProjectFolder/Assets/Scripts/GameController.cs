using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int level = 0;
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
        }

        SceneManager.LoadScene("Orbital_Level" + level);
    }
}
