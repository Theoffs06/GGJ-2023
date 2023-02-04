using System;
using UnityEngine;
using UnityEngine.AI;

public class Creature : Character {
    public int CurrentHealth { get; set; }
    
    [SerializeField] 
    private bool isKamikaze;
    public bool isTank;


    [Header("Defense")]
    [SerializeField] private int maxHealth;

    [Header("Attack")] 
    [SerializeField] private int attack;
    [SerializeField] private float rangeAttack; 
    [SerializeField] private float attackRate;

    private Transform _target;
    private NavMeshAgent _navMeshAgent;
    private float _time;
    private bool IsDead => CurrentHealth <= 0;

    protected override void Start() {
        base.Start();

        CurrentHealth = maxHealth;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindWithTag("Player").transform;
    }

    protected override void Update() {
        base.Update();

        if (IsDead) {
            Destroy(gameObject);
            return;
        }
        
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

                    if (_time >= attackRate) {
                        _target.GetComponent<PlayerCharacter>().HP -= attack;
                        _time = 0;
                    }
                    _time += Time.deltaTime;
                }
            }
        }

    }

    private void Boom() {
        _target.GetComponent<PlayerCharacter>().HP -= attack;
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }
}