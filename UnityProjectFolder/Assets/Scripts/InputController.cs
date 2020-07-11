using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public delegate void OnEmptySelection();
    public static OnEmptySelection emptySelection;

    [SerializeField]
    GameObject targetingRedicule;

    GameObject selectedObj = null;

    public void OnSelect()
    {
        // Mouse Raycast Too Select Object
        Ray worldRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(worldRay, out hit, 1000) && IsSelectable(hit.collider.gameObject))
        {
            selectedObj = hit.collider.gameObject;
            targetingRedicule.SetActive(true);

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

    public void OnTarget()
    {
        // Broadcast The Position In Worldspace
        if (selectedObj != null)
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            selectedObj.SendMessage("OnTarget", worldPos);

            Debug.Log("Target Position: " + worldPos);
        }
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
        return target.CompareTag("OrbitalTurret");
    }
}
