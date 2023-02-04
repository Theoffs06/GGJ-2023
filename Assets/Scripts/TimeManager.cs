using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    // TODO, make the time rewinding stuff decrease TimeCharge
    [SerializeField]private int crystalAmount = 0;
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
                ActivateBar();
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
        TimeCrystal.OnTimeCrystalCollected += IncrementTimeAndCrystal;
    }


    void OnDisable()
    {
        TimeCrystal.OnTimeCrystalCollected -= IncrementTimeAndCrystal;
    }

    private void IncrementTimeAndCrystal()
    {
        CrystalAmount = Mathf.Clamp(CrystalAmount + 1, 0, 4);

        if (timeBar.activeSelf)
            TimeCharge = Mathf.Clamp(TimeCharge + 20, 0, 100); ;
    }

    private void ActivateBar()
    {
        timeBar.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            if(TimeCharge >0)
            {
                IsRewinding = true;
                timer += Time.deltaTime;
                if(timer >= TimerMax)
                {
                    timer = 0;
                    TimeCharge -= TimeChargeDecreaseValue;
                }
            }
        }

        if (Input.GetKeyUp("space"))
        {
            IsRewinding = false;
            timer = 0;
        }
    }
}
