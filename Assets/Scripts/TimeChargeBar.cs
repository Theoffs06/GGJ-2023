using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeChargeBar : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image image;


    // Update is called once per frame
    void Update()
    {
        switch(timeManager.TimeCharge)
        {
            case int n when (n == 0):
                image.sprite = sprites[0];
                break;
            case int n when (n > 0 & n < 10):
                image.sprite = sprites[1];
                break;
            case int n when (n >= 10 & n < 20):
                image.sprite = sprites[2];
                break;
            case int n when (n >= 20 & n < 30):
                image.sprite = sprites[3];
                break;
            case int n when (n >= 30 & n < 40):
                image.sprite = sprites[4];
                break;
            case int n when (n >= 40 & n < 50):
                image.sprite = sprites[5];
                break;
            case int n when (n >= 50 & n < 60):
                image.sprite = sprites[6];
                break;
            case int n when (n >= 60 & n < 70):
                image.sprite = sprites[7];
                break;
            case int n when (n >= 70 & n < 80):
                image.sprite = sprites[8];
                break;
            case int n when (n >= 80 & n < 90):
                image.sprite = sprites[9];
                break;
            case int n when (n >= 90 & n < 100):
                image.sprite = sprites[10];
                break;
            case int n when (n == 100):
                image.sprite = sprites[11];
                break;


        }
    }
}
