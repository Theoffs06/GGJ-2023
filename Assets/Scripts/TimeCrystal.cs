using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCrystal : MonoBehaviour
{

    public delegate void CollectTimeCrystal();
    public static event CollectTimeCrystal OnTimeCrystalCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO, remove this when we can collect the crystal
        if (Input.GetKeyDown("space"))
        {
            if (OnTimeCrystalCollected != null)
                OnTimeCrystalCollected();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO Make it work with player Benoît is working on

        /*if (other.GetComponent<nameOfPlayerComponent>)
         */
        if (OnTimeCrystalCollected != null)
            OnTimeCrystalCollected();
    }
}
