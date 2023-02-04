using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCrystalBarUI : MonoBehaviour
{
    [SerializeField] private GameObject[] Crystals;
    [SerializeField] private TimeManager timeManager;
    private float timer;
    private float timerMax = 1;
    private bool isMax = false;


    void OnEnable()
    {
        TimeManager.OnUpdateCrystalAmount += UpdateUI;
    }


    void OnDisable()
    {
        TimeManager.OnUpdateCrystalAmount -= UpdateUI;
    }

    private void UpdateUI()
    {
        if (timeManager.CrystalAmount == 0)
        {
            Crystals[3].SetActive(true);
            isMax = true;
            timer = 0;
        }
        else
        {
            Crystals[timeManager.CrystalAmount - 1].SetActive(true);
            for (int i = 0; i <= timeManager.CrystalAmount - 1 ; i++)
            {
                if(!Crystals[i].activeSelf)
                    Crystals[i].SetActive(true);

            }
        }

    }

    private void Update()
    {
        if(isMax)
        {
            if (timer <= timerMax)
                timer+= Time.deltaTime;
            else
            {
                foreach (GameObject crystal in Crystals)
                    crystal.SetActive(false);
                isMax = false;
            }

        }

    }


}
