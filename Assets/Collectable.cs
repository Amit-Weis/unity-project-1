using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public UnityEvent coinCollected;

    private bool collected = false;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            Debug.Log("once or twice");
            GameObject self = gameObject;
            coinCollected.Invoke();
            Destroy(self);
        }
        collected = true;
    }

    public void SetGameManager(GameManagaer manager)
    {
        coinCollected.AddListener(manager.coinCollected);
    }

}
