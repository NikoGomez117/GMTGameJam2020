using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ACT_InputManager : MonoBehaviour
{
    public delegate void OnEmptySelection();
    public static OnEmptySelection emptySelection;

    public delegate void OnEmptyTarget();
    public static OnEmptyTarget emptyTarget;

    public delegate void OnSelectTurret();
    public static OnSelectTurret selectTurret;

    public delegate void OnSelectScrap();
    public static OnSelectScrap selectScrap;

    [SerializeField]
    GameObject targetingRedicule;

    public static GameObject selectedObj = null;

    public void OnSelect()
    {
        // Mouse Raycast Too Select Object
        Ray worldRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.GetRayIntersection(worldRay);

        if (hit && IsSelectable(hit.collider.gameObject))
        {
            SelectObject(hit.collider.gameObject);
        }
        else
        {
            selectedObj = null;
            targetingRedicule.SetActive(false);

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
                selectTurret?.Invoke();
                break;
            case "Scrap":
                obj.SendMessage("OnPickup");
                selectScrap?.Invoke();
                break;
        }
    }

    public void OnTarget()
    {
        emptyTarget?.Invoke();
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnSkipLevel()
    {
#if UNITY_EDITOR
        OBV_LevelManager.instance.ChangeLevel();
#endif
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
