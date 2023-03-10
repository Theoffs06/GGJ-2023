using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class WeaponMinigun : Weapon
{
    [SerializeField]
    private Bullet m_BulletPrefab = null;

    [SerializeField]
    private float m_DelayBetweenBullets = 0.2f;
    private float m_CurrentDelay = 0f;

    [Header("Audio")] 
    [SerializeField] private StudioEventEmitter actionEvent;

    [SerializeField]
    private float m_Duration = 0.1f;

    private ScreenShake m_ScreenShake = null;

    private void Start()
    {
        m_ScreenShake = FindObjectOfType<ScreenShake>();
    }

    public override void StartShooting()
    {
        base.StartShooting();
        if (!actionEvent.IsPlaying()) {
            actionEvent.SetParameter("Auto", 0f);
            actionEvent.Play();
        }
    }

    public override void StopShooting()
    {
        base.StopShooting();
        m_CurrentDelay = 0f;
        if (actionEvent.IsPlaying()) {
            actionEvent.SetParameter("Auto", 0.15f);
        }
    }

    public void Update()
    {
        if (m_IsShooting)
        {
            if (Mathf.Approximately(m_CurrentDelay, 0f))
            {
                Instantiate<Bullet>(m_BulletPrefab, transform.position, Quaternion.LookRotation(transform.forward));
            }

            if (m_ScreenShake)
            {
                m_ScreenShake.Shake(m_Duration);
            }

            m_CurrentDelay += Time.deltaTime;

            Debug.Log(m_CurrentDelay);

            if (m_CurrentDelay >= m_DelayBetweenBullets)
            {
                m_CurrentDelay = 0f;
            }
        }
    }
}
