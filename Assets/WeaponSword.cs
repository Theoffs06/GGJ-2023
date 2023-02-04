using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    public override void StartShooting()
    {
        base.StartShooting();
    }

    public override void StopShooting()
    {
        base.StopShooting();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Creature>().CurrentHealth -= 1;
        }

    }
}
