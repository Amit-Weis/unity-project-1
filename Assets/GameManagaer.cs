using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManagaer : MonoBehaviour
{
    public Text speedCounter;
    public Text coinCounter;
    public GameObject bird;
    private int counterNum = 0;
    public GameObject collectablePrefab;
    public AudioSource lowstakes;
    public AudioSource highstakes;
    public AudioSource crash;
    private float time = 0;
    private bool switchSong = false;
    private bool alive = true;
    private float mappedSpeed;
    private List<Vector3> coinPositions = new List<Vector3>();
    // Start is called before the first frame update

    private void Start()
    {
        coinPositions.Add(new Vector3(-98.1f, 25.6f, 0));
        coinPositions.Add(new Vector3(84.5f, 32.9f, 0));
    }

    private void Update()
    {
        time += Time.deltaTime;

        float noise = Mathf.Pow(mappedSpeed / 100.0f, 2);

        lowstakes.volume = noise;
        highstakes.volume = noise;

        if (time >= 1.8462 * 4)
        {
            if (switchSong && alive)
            {
                lowstakes.enabled = false;
                highstakes.enabled = true;
                switchSong = false;
            }
            time = 0;
        }
    }
    public void coinCollected()
    {
        counterNum += 1;
        coinCounter.text = counterNum.ToString();
        InstantiateCollectable();
        switchSong = true;
    }

    public void currentSpeedAnnoucment(float currentSpeed, float maxSpeed)
    {
        mappedSpeed = (Mathf.Clamp(currentSpeed, 0, float.PositiveInfinity) / maxSpeed) * 100;

        speedCounter.text = ((int)mappedSpeed).ToString();
    }

    public void InstantiateCollectable()
    {
        if (coinPositions.Count != 0)
        {
            Vector3 coinPosition = coinPositions[coinPositions.Count - 1];
            coinPositions.RemoveAt(coinPositions.Count - 1);
            GameObject collectableInstance = Instantiate(collectablePrefab, coinPosition, Quaternion.identity);
            Collectable collectableScript = collectableInstance.GetComponent<Collectable>();
            collectableScript.SetGameManager(this);
        }
        else
        {
            coinCounter.text = "YUO WON YAYYYY";
        }
    }

    public void birdDead()
    {
        lowstakes.enabled = false;
        highstakes.enabled = false;
        crash.enabled = true;
        alive = false;
    }

}

