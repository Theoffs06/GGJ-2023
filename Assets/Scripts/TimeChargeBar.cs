using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeChargeBar : MonoBehaviour
{
    [SerializeField] private Slider timeChargeBar;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private TMP_Text percentage;


    // Update is called once per frame
    void Update()
    {
        timeChargeBar.value = timeManager.TimeCharge;
        percentage.text = timeManager.TimeCharge + "%";
    }
}
