using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashTriggerHandler : MonoBehaviour
{
    private bool isCrashed = false;

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Trace")
        {
            isCrashed = true;
        }
    }

    public bool IsCrashed()
    {
        return isCrashed;
    }
}
