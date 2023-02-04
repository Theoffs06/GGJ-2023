using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{
    public float TimerCooldownSwordMax = 3;
    private float timerCooldownSword = 0;
    private bool activated = true;
    public List<Creature> creaturesInsideCircle = new List<Creature>();
    public override void StartShooting()
    {
        base.StartShooting();
    }

    public override void StopShooting()
    {
        base.StopShooting();
    }
    private void OnDisable()
    {
        creaturesInsideCircle.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(activated)
        {
            foreach (Creature enemy in creaturesInsideCircle.ToArray())
            {
                if (!enemy.gameObject.GetComponent<Creature>().isTank)
                {
                    creaturesInsideCircle.Remove(enemy);
                    Destroy(enemy.gameObject);
                }
                else
                    enemy.gameObject.GetComponent<Creature>().CurrentHealth -= 8;

                timerCooldownSword = 0;
                activated = false;

            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Creature>())
            creaturesInsideCircle.Add(other.GetComponent<Creature>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.GetComponent<Creature>())
            creaturesInsideCircle.Remove(other.GetComponent<Creature>());
    }

    private void Update()
    {
        if(!activated)
        {
            timerCooldownSword += Time.deltaTime;
            if (timerCooldownSword >= TimerCooldownSwordMax)
                activated = true;

        }

        /*for (int i = 0; i<= creaturesInsideCircle.Count-1; i++)
        {
            if (creaturesInsideCircle[i] == null)
                creaturesInsideCircle.RemoveAt(i);

        }*/
    }
}
