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
    [SerializeField]
    Transform ammoGage;

    protected override void Subscribe()
    {
        InputController.emptySelection += EmptySelectionEvent;
        InputController.emptyTarget += EmptyTargetEvent;
    }

    protected override void UnSubscribe()
    {
        InputController.emptySelection -= EmptySelectionEvent;
        InputController.emptyTarget -= EmptyTargetEvent;
    }

    protected override void Start()
    {
        base.Start();
        InitOrbitRenderer();
    }

    private void Update()
    {
        UpdateHighlight();
        RepositionAmmoGage();
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

        if (/*!myTurret.activeSelf && */ Mathf.Abs(Vector2.Distance(worldPos, Vector2.zero) - myRadius) < zoneThickness)
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

            /*if (InputController.selectedObj == myTurret)
            {
                placementHighlight.SetActive(true);

                placementHighlight.transform.position = worldPos.normalized * myRadius;
                placementHighlight.transform.right = worldPos.normalized;
            }*/
        }
    }

    void RepositionAmmoGage()
    {
        ammoGage.position = myTurret.transform.position + Vector3.up * 0.5f;
    }

    void EmptySelectionEvent()
    {
        /*Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (myTurret.activeSelf && InputController.selectedObj != myTurret && Mathf.Abs(Vector2.Distance(worldPos, Vector2.zero) - myRadius) < zoneThickness)
        {
            InputController.instance.SelectObject(myTurret);
        }*/

        /*
        if (!myTurret.activeSelf && Mathf.Abs(Vector2.Distance(worldPos, Vector2.zero) - myRadius) < zoneThickness)
        {
            myTurret.transform.position = worldPos.normalized * myRadius;
            myTurret.transform.right = worldPos.normalized;
            myTurret.SetActive(true);
        }*/
    }

    void EmptyTargetEvent()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (myTurret.activeSelf && Mathf.Abs(Vector2.Distance(worldPos, Vector2.zero) - myRadius) < zoneThickness)
        {
            myTurret.SendMessage("OnTarget", worldPos);
        }
    }
}
