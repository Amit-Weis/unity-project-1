using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManagaer : MonoBehaviour
{
    //*
    // Public

    // UI Elements
    public Text speedCounter;
    public Text coinCounter;
    public Image coinCounterImage;

    // Audio sources
    public AudioSource lowstakes;
    public AudioSource highstakes;
    public AudioSource crash;
    public AudioSource chime;

    // Game objects
    public GameObject bird;
    public GameObject collectablePrefab;
    public GameObject coinObjects;

    // sprites
    public Sprite onecoins;
    public Sprite twocoins;

    //*
    // Private

    // music variables
    private float mappedSpeed;
    private float noiseModifier = 0f;
    private bool switchSong = false;

    // logic variables
    private bool alive = true;

    // coin variables
    private List<Vector3> coinPositions = new List<Vector3>();
    private List<Sprite> coinUI = new List<Sprite>();

    private void Start()
    {
        coinUI.Add(twocoins);
        coinUI.Add(onecoins);
        coinPositions.Add(new Vector3(-98.1f, 25.6f, 0));
        coinPositions.Add(new Vector3(-57f, -15f, 0));
        InstantiateCollectable();
    }

    private void Update()
    {

        updateMusicVolume();

        if (switchSong && alive)
        {
            switchSongs();
        }
    }

    private void updateMusicVolume()
    {
        float noise = Mathf.Pow(mappedSpeed / 100.0f, 2) + noiseModifier;

        lowstakes.volume = noise;
        highstakes.volume = noise;
    }

    private void switchSongs()
    {
        lowstakes.enabled = false;
        highstakes.enabled = true;
        switchSong = false;
        noiseModifier = 0.2f;
    }
    public void coinCollected()
    {
        playChime();
        if (coinUI.Count == 2)
        {
            activateCoinObstacles();
        }
        updateCoinUI();
        InstantiateCollectable();
        switchSong = true;
    }

    private void activateCoinObstacles()
    {
        Transform[] coinObstacles = coinObjects.GetComponentsInChildren<Transform>(true);
        Debug.Log(coinObstacles.Length);
        foreach (Transform obstacle in coinObstacles)
        {
            if (obstacle != coinObjects.transform) // Ensure we are not toggling the root object itself
            {
                GameObject obstacleGameObject = obstacle.gameObject;
                Debug.Log(obstacleGameObject.activeSelf);
                obstacleGameObject.SetActive(!obstacleGameObject.activeSelf);
            }
        }

    }

    private void playChime()
    {
        chime.enabled = false;
        chime.enabled = true;
    }

    private void updateCoinUI()
    {
        coinCounterImage.sprite = coinUI[coinUI.Count - 1];
        coinUI.RemoveAt(coinUI.Count - 1);
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
    }

    public void birdDead()
    {
        lowstakes.enabled = false;
        highstakes.enabled = false;
        crash.enabled = true;
        alive = false;
    }

}