using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimeCrystalBarUI : MonoBehaviour
{
    [SerializeField] private TMP_Text CrystalAmountText;
    [SerializeField] private TimeManager timeManager;
    void OnEnable()
    {
        TimeCrystal.OnTimeCrystalCollected += IncrementUI;
    }


    void OnDisable()
    {
        TimeCrystal.OnTimeCrystalCollected -= IncrementUI;
    }
    
    private void IncrementUI()
    {
        CrystalAmountText.text = "Crystals = " + timeManager.CrystalAmount;
    }


}
