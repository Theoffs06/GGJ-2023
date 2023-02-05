using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float shake = 0f;
    [SerializeField] private float shakeAmount = 0.7f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = camera.transform.localPosition;
    }

    private void Update()
    {
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
