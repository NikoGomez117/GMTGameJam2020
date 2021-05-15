﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ACT_Scrap : Actor
{
    public delegate void OnScrapPickup(Vector2 pos);
    public static OnScrapPickup scrapPickup;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 0.05f,ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(0.05f,0.1f),ForceMode2D.Impulse);

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);

        transform.right = Random.insideUnitCircle;

        Camera.main.GetComponent<OBV_CameraManager>().ShakeScreen(2f,0.25f);
    }

    public void OnPickup()
    {
        ((OBV_Homeworld)OBV_Homeworld.instance).AddScrap(1);
        scrapPickup?.Invoke(transform.position);
        Destroy(gameObject);
    }
}