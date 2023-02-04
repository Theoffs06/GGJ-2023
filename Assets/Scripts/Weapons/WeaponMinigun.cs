using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMinigun : Weapon
{
    [SerializeField]
    private Bullet m_BulletPrefab = null;

    [SerializeField]
    private float m_DelayBetweenBullets = 0.2f;
    private float m_CurrentDelay = 0f;

    public override void StartShooting()
    {
        base.StartShooting();

        
    }

    public override void StopShooting()
    {
        base.StopShooting();
        m_CurrentDelay = 0f;
    }

    public void Update()
    {
        if (m_IsShooting)
        {
            if (Mathf.Approximately(m_CurrentDelay, 0f))
            {
                Instantiate<Bullet>(m_BulletPrefab, transform.position, Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, -90, 0));
            }

            m_CurrentDelay += Time.deltaTime;

            if (m_CurrentDelay >= m_DelayBetweenBullets)
            {
                m_CurrentDelay = 0f;
            }
        }
    }
}
