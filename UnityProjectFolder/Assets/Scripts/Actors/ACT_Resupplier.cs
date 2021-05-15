using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT_Resupplier : Actor
{
    public delegate void OnReSupply();
    public static OnReSupply reSupply;
    
    GameObject prvTurret;
    float ammoResupply = 2.5f;
    float scrapDrain = 1f;
    float iterations = 10f;

    IEnumerator myRoutine;

    void Update()
    {
        CheckResupply();
    }

    void CheckResupply()
    {
        if (ACT_InputManager.selectedObj != prvTurret)
        {
            if (myRoutine != null)
            {
                StopCoroutine(myRoutine);
            }
            prvTurret = ACT_InputManager.selectedObj;

            if (CheckIsOrbitalTurret())
            {
                myRoutine = ReSupplyRoutine();
                StartCoroutine(myRoutine);
            }
        }

        if (CheckIsOrbitalTurret())
        {
            GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, prvTurret.transform.position });
        }
    }

    bool CheckIsOrbitalTurret()
    {
        return prvTurret != null && prvTurret.CompareTag("OrbitalTurret");
    }

    bool CheckScrapAmmoState()
    {
        return !prvTurret.GetComponent<ACT_OrbitalTurrent>().IsMaxAmmo() && (((OBV_Homeworld)OBV_Homeworld.instance).GetScrapRemainder() > 0 || ((OBV_Homeworld)OBV_Homeworld.instance).GetScrap() > 0);
    }
    IEnumerator ReSupplyRoutine()
    {
        float iterDelta = 1f / iterations;

        while (true)
        {
            yield return new WaitForSeconds(iterDelta);

            if (CheckScrapAmmoState())
            {
                GetComponent<LineRenderer>().enabled = true;
                prvTurret.GetComponent<ACT_OrbitalTurrent>().Ammo += iterDelta * ammoResupply;
                ((OBV_Homeworld)OBV_Homeworld.instance).AddScrapRemainder(-iterDelta * scrapDrain);
                reSupply?.Invoke();
            }

            if (!CheckScrapAmmoState())
            {
                GetComponent<LineRenderer>().enabled = false;
            }
        }
    }
}
