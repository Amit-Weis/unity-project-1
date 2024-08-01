using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Updraft : MonoBehaviour
{
    public float windFactor = 6;
    public UnityEvent<float> inUpdraft;

    void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("uppies");
        /*rb.AddForce(Vector2.up * windFactor);*/
        inUpdraft.Invoke(windFactor);
    }

}
