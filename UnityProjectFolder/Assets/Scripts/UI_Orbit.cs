using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Orbit : SubscribingMonoBehaviour
{
    float zoneThickness = 0.5f;

    [SerializeField]
    float myRadius = 5f / 4f;
    [SerializeField]
    GameObject placementHighlight;
    [SerializeField]
    GameObject myTurret;

    protected override void Subscribe()
    {
        InputController.emptySelection += EmptySelectionEvent;
    }

    protected override void UnSubscribe()
    {
        InputController.emptySelection -= EmptySelectionEvent;
    }

    protected override void Start()
    {
        base.Start();
        InitOrbitRenderer();
    }

    private void Update()
    {
        UpdateHighlight();
    }

    void InitOrbitRenderer()
    {
        MaterialPropertyBlock myBlock = new MaterialPropertyBlock();
        myBlock.Clear();
        myBlock.SetColor("_Color", Color.cyan);
        myBlock.SetFloat("_Distance", myRadius);

        GetComponent<SpriteRenderer>().SetPropertyBlock(myBlock);
    }

    void UpdateHighlight()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (!myTurret.activeSelf && Mathf.Abs(Vector2.Distance(worldPos, Vector2.zero) - myRadius) < zoneThickness)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            placementHighlight.SetActive(true);

            placementHighlight.transform.position = worldPos.normalized * myRadius;
            placementHighlight.transform.right = worldPos.normalized;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            placementHighlight.SetActive(false);
        }
    }

    void EmptySelectionEvent()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (!myTurret.activeSelf && Mathf.Abs(Vector2.Distance(worldPos, Vector2.zero) - myRadius) < zoneThickness)
        {
            myTurret.transform.position = worldPos.normalized * myRadius;
            myTurret.transform.right = worldPos.normalized;
            myTurret.SetActive(true);
        }
    }
}
