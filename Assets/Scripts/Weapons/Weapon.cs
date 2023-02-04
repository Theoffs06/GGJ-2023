using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected bool m_IsShooting = false;

    [SerializeField]
    protected float m_DistanceFromPlayer = 10f;

    public void SetWeaponRotation(Vector2 stickDirection)
    {
        if (stickDirection.sqrMagnitude > 0f)
        {
            Vector3 direction = Vector3.zero;
            direction.x = stickDirection.x;
            direction.z = -stickDirection.y;

            transform.rotation = Quaternion.LookRotation(direction);
            transform.localPosition = direction * m_DistanceFromPlayer;
        }
    }

    public virtual void StartShooting()
    {
        m_IsShooting = true;
    }

    public virtual void StopShooting()
    {
        m_IsShooting = false;
    }
}
