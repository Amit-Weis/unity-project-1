using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagaer : MonoBehaviour
{
    public Text speedCounter;
    public Text coinCounter;
    public GameObject bird;
    private int counterNum = 0;
    public GameObject collectablePrefab;

    private List<Vector3> coinPositions = new List<Vector3>();
    // Start is called before the first frame update

    private void Start()
    {
        coinPositions.Add(new Vector3(-98.1f, 25.6f, 0));
        coinPositions.Add(new Vector3(84.5f, 32.9f, 0));
    }
    public void coinCollected()
    {
        counterNum += 1;
        coinCounter.text = counterNum.ToString();
        InstantiateCollectable();
    }

    public void currentSpeedAnnoucment(float currentSpeed, float maxSpeed)
    {
        float mappedSpeed = (Mathf.Clamp(currentSpeed, 0, float.PositiveInfinity) / maxSpeed) * 100;

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

}

