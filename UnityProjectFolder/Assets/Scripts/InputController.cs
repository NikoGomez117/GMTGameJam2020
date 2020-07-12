using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    public delegate void OnEmptySelection();
    public static OnEmptySelection emptySelection;

    public delegate void OnEmptyTarget();
    public static OnEmptyTarget emptyTarget;

    [SerializeField]
    GameObject targetingRedicule;

    public static GameObject selectedObj = null;

    [SerializeField]
    AudioSource selectSound;
    [SerializeField]
    AudioSource targetSound;
    [SerializeField]
    AudioSource pickupSound;

    private void Awake()
    {
        instance = this;
    }

    public void OnSelect()
    {
        // Mouse Raycast Too Select Object
        Ray worldRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.GetRayIntersection(worldRay);

        if (hit && IsSelectable(hit.collider.gameObject))
        {
            SelectObject(hit.collider.gameObject);

            Debug.Log("Selected Object: " + selectedObj.name);
        }
        else
        {
            selectedObj = null;
            targetingRedicule.SetActive(false);

            Debug.Log("No Object Selected");

            emptySelection?.Invoke();
        }
    }

    public void SelectObject(GameObject obj)
    {
        switch (obj.tag)
        {
            case "OrbitalTurret":
                selectedObj = obj;
                targetingRedicule.SetActive(true);
                selectSound.Play();
                break;
            case "Scrap":
                pickupSound.Play();
                obj.SendMessage("OnPickup");
                break;
        }
    }

    public void OnTarget()
    {
        // Broadcast The Position In Worldspace
        /*if (selectedObj != null)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            selectedObj.SendMessage("OnTarget", worldPos);

            targetSound.Play();

            Debug.Log("Target Position: " + worldPos);
        }*/

        emptyTarget?.Invoke();
    }

    private void Update()
    {
        UpdateRedicule();
    }

    void UpdateRedicule()
    {
        if (selectedObj != null)
        {
            targetingRedicule.transform.position = selectedObj.transform.position;
        }
    }

    public bool IsSelectable(GameObject target)
    {
        return target.CompareTag("OrbitalTurret") 
            || target.CompareTag("Scrap");
    }
}
