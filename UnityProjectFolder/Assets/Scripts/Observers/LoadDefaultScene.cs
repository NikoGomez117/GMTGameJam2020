using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDefaultScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
#if UNITY_EDITOR
        if (OBV_LevelManager.instance == null)
        {
            SceneManager.LoadScene("GameController");
        }
#endif
    }
}
