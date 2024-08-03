using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public UnityEvent coinCollected;
    public AudioSource chime;

    private bool collected = false;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            chime.enabled = true;
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
