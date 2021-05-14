using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OBV_UI_Homeworld_Scrap : Observer
{
    [SerializeField]
    Image myRenderer;
    [SerializeField]
    Animator myAnim;

    IEnumerator lineDrawer = null;

    protected override void Subscribe()
    {
        ACT_HomeworldStats.scrapChanged += ScrapChangedEvent;
        ACT_HomeworldStats.scrapRemainderChanged += ScrapRemainderChangedEvent;

        Scrap.scrapPickup += ScrapPickupEvent;
    }

    protected override void UnSubscribe()
    {
        ACT_HomeworldStats.scrapChanged -= ScrapChangedEvent;
        ACT_HomeworldStats.scrapRemainderChanged -= ScrapRemainderChangedEvent;

        Scrap.scrapPickup -= ScrapPickupEvent;
    }

    private void ScrapChangedEvent(float delta)
    {
        myRenderer.sprite = OBV_UI_Controller.instance.numSprites[((OBV_Homeworld)OBV_Homeworld.instance).GetScrap()];

        if (delta < 0)
        {
            myAnim.Play("SpendScrap");
        }
        else if (delta > 0)
        {
            myAnim.Play("GainScrap");
        }
    }

    private void ScrapRemainderChangedEvent(float val)
    {
        GetComponent<Image>().fillAmount = Mathf.Max(val,0f);
    }

    void ScrapPickupEvent(Vector2 target)
    {
        if (lineDrawer != null)
            StopCoroutine(lineDrawer);

        lineDrawer = DrawLine(target);

        StartCoroutine(lineDrawer);
    }

    IEnumerator DrawLine(Vector2 target)
    {
        GetComponent<LineRenderer>().SetPositions(
            new Vector3[]
            {
                transform.position,
                target
            }
            );

        GetComponent<LineRenderer>().enabled = true;

        yield return new WaitForSeconds(0.25f);

        GetComponent<LineRenderer>().enabled = false;
    }
}
