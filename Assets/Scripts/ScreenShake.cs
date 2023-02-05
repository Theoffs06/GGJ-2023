using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float shake = 0f;
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private Slider slider;

    private Vector3 startPos;

    private void Start()
    {
        camera = Camera.main;
        startPos = camera.transform.localPosition;
        if (GameObject.Find("ScreenShakeSlider"))
        {
            slider = GameObject.Find("ScreenShakeSlider").GetComponent<Slider>();
            slider.value = shakeAmount;
        }

    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(camera == null)
        {
            camera = Camera.main;
            startPos = camera.transform.localPosition;
        }

        if (slider == null)
        {
            if(GameObject.Find("ScreenShakeSlider"))
            {
                slider = GameObject.Find("ScreenShakeSlider").GetComponent<Slider>();
                slider.value = shakeAmount;
            }

        }
        else
            shakeAmount = slider.value;
        if (shake > 0)
        {
            camera.transform.localPosition = startPos + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime;
        }
        else
        {
            shake = 0.0f;
        }
    }

    public void Shake(float duration)
    {
        shake = duration;
    }

}
