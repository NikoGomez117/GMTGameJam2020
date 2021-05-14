using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    bool subscribed = false;

    public static object instance;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        SubscribeSwitch(true);
    }

    protected virtual void OnEnable()
    {
        SubscribeSwitch(true);
    }

    protected virtual void OnDisable()
    {
        SubscribeSwitch(false);
    }

    protected virtual void OnDestroy()
    {
        SubscribeSwitch(false);
    }

    protected virtual void SubscribeSwitch(bool subscribing)
    {
        if (subscribed && !subscribing)
        {
            subscribed = false;
            UnSubscribe();
        }
        else if (!subscribed && subscribing)
        {
            subscribed = true;
            Subscribe();
        }
        else
        {
            return;
        }
    }

    protected virtual void Subscribe()
    {
        return;
    }

    protected virtual void UnSubscribe()
    {
        return;
    }
}
