using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public UnityEvent death;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        death.Invoke();
    }
}
