using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private enum CamMove
    {
        STOP,
        ZOOM,
        DEZOOM
    }

    private CamMove movement;
    public void ZoomCam()
    {
        movement = CamMove.ZOOM;
    }

    public void DeZoomCam()
    {
        movement = CamMove.DEZOOM;
    }

    public void StopZoom()
    {
        movement = CamMove.STOP;
    }

    private void Update()
    {
        if (movement != CamMove.STOP)
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + (movement == CamMove.ZOOM ? -0.1f : 0.1f), 2, 30);
    }
}
