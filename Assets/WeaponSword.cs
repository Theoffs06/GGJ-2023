using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    public float TimerCooldownSwordMax = 3;
    private float timerCooldownSword = 0;
    private bool activated = true;
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
        if (collision.gameObject.CompareTag("Enemy") && activated)
        {
            Destroy(collision.gameObject);
            timerCooldownSword = 0;
            activated = false;
        }

    }

    private void Update()
    {
        if(!activated)
        {
            timerCooldownSword += Time.deltaTime;
            if (timerCooldownSword >= TimerCooldownSwordMax)
                activated = true;

        }
    }
}
