using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OBV_UI_Orbit : Observer
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

    [SerializeField]
    AudioSource targetSound;

    static GameObject orbitLock = null;

    private void Awake()
    {
        orbitLock = null;
    }

    protected override void Subscribe()
    {
        ACT_InputManager.emptySelection += EmptySelectionEvent;
        ACT_InputManager.emptyTarget += EmptyTargetEvent;
    }

    protected override void UnSubscribe()
    {
        ACT_InputManager.emptySelection -= EmptySelectionEvent;
        ACT_InputManager.emptyTarget -= EmptyTargetEvent;
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
        myBlock.SetColor("_Color", GetComponent<SpriteRenderer>().material.color);
        myBlock.SetFloat("_Distance", myRadius);

        GetComponent<SpriteRenderer>().SetPropertyBlock(myBlock);
    }

    void UpdateHighlight()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (/*!myTurret.activeSelf && */ Mathf.Abs(Vector2.Distance(worldPos, transform.position) - myRadius) < zoneThickness && (orbitLock == null || orbitLock == gameObject))
        {
            orbitLock = gameObject;

            GetComponent<SpriteRenderer>().color = Color.white - new Color(0,0,0,0.25f);
            placementHighlight.SetActive(true);

            placementHighlight.transform.localPosition = transform.InverseTransformPoint(worldPos).normalized * myRadius;
            placementHighlight.transform.right = transform.InverseTransformPoint(worldPos).normalized;
        }
        else if(orbitLock == gameObject)
        {
            orbitLock = null;

            GetComponent<SpriteRenderer>().color = Color.white - new Color(0, 0, 0, 0.75f);
            placementHighlight.SetActive(false);
        }
    }

    void RepositionAmmoGage()
    {
        ammoGage.position = myTurret.transform.position - Vector3.up * 0.5f;
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

        if (myTurret.activeSelf && Mathf.Abs(Vector2.Distance(worldPos, transform.position) - myRadius) < zoneThickness && orbitLock == gameObject)
        {
            targetSound.Play();
            myTurret.SendMessage("OnTarget", worldPos);
        }
    }
}
