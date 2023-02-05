using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class TimeCrystal : MonoBehaviour
{
    [Header("Audio")] 
    [SerializeField] private StudioEventEmitter collectEvent;
    
    public delegate void CollectTimeCrystal();
    public static event CollectTimeCrystal OnTimeCrystalCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerCharacter>())
        {
            if (OnTimeCrystalCollected != null)
                OnTimeCrystalCollected();
            collectEvent.Play();
            Destroy(gameObject);
        }

    }
}
