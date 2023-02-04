using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrystalDirector : MonoBehaviour
{

    private float timerBeforeSpawn = 0;
    public float TimerBeforeSpawnMin = 10; public float TimerBeforeSpawnMaxTreshold = 15;
    private float timerBeforeSpawnMax;
    [SerializeField] private GameObject Crystal;
    [Header("Size")] 
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    // Start is called before the first frame update
    void Start()
    {
        timerBeforeSpawnMax = initTimerBeforeSpawnMax();
    }

    private float initTimerBeforeSpawnMax()
    {
        return Random.Range(TimerBeforeSpawnMin, TimerBeforeSpawnMaxTreshold);
    }
    // Update is called once per frame
    void Update()
    {
        timerBeforeSpawn+= Time.deltaTime;
        if(timerBeforeSpawn >= timerBeforeSpawnMax)
        {
            Vector3 pos = new Vector3(Random.Range(-sizeX, sizeX), 0, Random.Range(-sizeY, sizeY));
            Instantiate(Crystal, pos, new Quaternion(0,0,0,0));
            timerBeforeSpawn = 0;
            timerBeforeSpawnMax = initTimerBeforeSpawnMax();

        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, 1, sizeY));
    }
}
