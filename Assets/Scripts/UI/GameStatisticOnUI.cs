using System;
using UnityEngine.UI;
using UnityEngine;

public class GameStatisticOnUI : MonoBehaviour
{
    [SerializeField] private Text fpsCounter;
    
    void Update()
    {
        fpsCounter.text = ($"fps {Convert.ToInt32(1f/ Time.deltaTime)}");
    }
}
