using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class coinObstacle : MonoBehaviour
{
    public UnityEvent coindeath;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        coindeath.Invoke();
    }
}
