using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChargeBar : MonoBehaviour
{
    [SerializeField] private Slider timeChargeBar;
    [SerializeField] private TimeManager timeManager;


    // Update is called once per frame
    void Update()
    {
        timeChargeBar.value = timeManager.TimeCharge;
    }
}
