using System;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour {
    [SerializeField] 
    private bool isKamikaze;
    
    [Header("Defense")]
    [SerializeField] private int maxHealth;
    [NonSerialized] public int currentHealth;

    [Header("Attack")] 
    [SerializeField] private float attack;
    [SerializeField] private float rangeAttack; 
    [SerializeField] private float attackRate;

    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private float _time;

    private void Start() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if(GameObject.Find("TimeManager"))
        {
            if(!GameObject.Find("TimeManager").GetComponent<TimeManager>().IsRewinding)
            {
                _navMeshAgent.destination = _target.position;
                if (Vector3.Distance(transform.position, _target.position) <= rangeAttack)
                {
                    if (isKamikaze)
                    {
                        Boom();
                        return;
                    }

                    if (_time >= attackRate)
                    {
                        Debug.Log("Attack" + " : " + attack);
                        _time = 0;
                    }
                    _time += Time.deltaTime;
                }
            }
        }

    }

    private void Boom() {
        Debug.Log("Boom");
        Destroy(gameObject);
    }
}