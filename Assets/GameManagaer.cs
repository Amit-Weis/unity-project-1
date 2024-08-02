using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagaer : MonoBehaviour
{
    public Text counter;
    private int counterNum = 0;
    // Start is called before the first frame update
    public void coinCollected()
    {
        counterNum += 1;

        counter.text = counterNum.ToString();
    }
    
}

