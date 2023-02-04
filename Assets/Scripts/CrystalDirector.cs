using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDirector : MonoBehaviour
{

    private float timerBeforeSpawn = 0;
    public float TimerBeforeSpawnMin = 10; public float TimerBeforeSpawnMaxTreshold = 15;
    private float timerBeforeSpawnMax;
    [SerializeField] private GameObject Crystal;

    [Header("Place UpperLeftCornerSpawn first, then BottomRightCornerSpawn")]
    [SerializeField] private GameObject[] Corners;
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
            Vector3 pos = new Vector3(Random.Range(Corners[0].transform.position.x, Corners[1].transform.position.x), 0, Random.Range(Corners[0].transform.position.z, Corners[1].transform.position.z));
            GameObject.Instantiate(Crystal, pos, new Quaternion(0,0,0,0));
            timerBeforeSpawn = 0;
            timerBeforeSpawnMax = initTimerBeforeSpawnMax();

        }
    }
}
