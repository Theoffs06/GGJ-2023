using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]private int crystalAmount = 0;
    public delegate void CrystalAmountUpdated();
    public static event CrystalAmountUpdated OnUpdateCrystalAmount;

    public int CrystalAmount
    {
        get
        {
            return crystalAmount;
        }
        set
        {
            crystalAmount = value;
            if (CrystalAmount >= CrystalReqForActivation)
            {
                FillBar();
                crystalAmount = 0;
            }
            if (OnUpdateCrystalAmount != null)
                OnUpdateCrystalAmount();


        }
    }
    public int CrystalReqForActivation = 4;

    [SerializeField] private GameObject timeBar;

    public int TimeCharge;

    private float timer;
    public float TimerMax = 1;
    public int TimeChargeDecreaseValue = 2;
    public bool IsRewinding = false;

    void OnEnable()
    {
        TimeCrystal.OnTimeCrystalCollected += IncrementCrystal;
    }


    void OnDisable()
    {
        TimeCrystal.OnTimeCrystalCollected -= IncrementCrystal;
    }

    private void IncrementCrystal()
    {
        CrystalAmount = Mathf.Clamp(CrystalAmount + 1, 0, 4);
    }

    private void FillBar()
    {
        TimeCharge = Mathf.Clamp(TimeCharge + 20, 0, 100); ;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRewinding)
        {
            timer += Time.deltaTime;
            if (timer >= TimerMax)
            {
                timer = 0;
                TimeCharge -= TimeChargeDecreaseValue;
            }
        }
        else
        {
            timer = 0;
        }
        /* if (Input.GetKey("space"))
         {
             if (TimeCharge > 0)
             {
                 rewindValue += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

                 if (!IsRewinding)
                 {
                     rewindManager.StartRewindTimeBySeconds(rewindValue);
                 }
                 else
                 {
                     if (rewindManager.HowManySecondsAvailableForRewind > rewindValue)      //Safety check so it is not grabbing values out of the bounds
                         rewindManager.SetTimeSecondsInRewind(rewindValue);
                 }
                 IsRewinding = true;
                 timer += Time.deltaTime;
                 if (timer >= TimerMax)
                 {
                     timer = 0;
                     TimeCharge -= TimeChargeDecreaseValue;
                 }
             }
         }
         else
         {
             if (IsRewinding)
             {
                 rewindManager.StopRewindTimeBySeconds();
                 rewindValue = 0;
                 IsRewinding = false;
                 timer = 0;
             }
         }*/
    }
}
