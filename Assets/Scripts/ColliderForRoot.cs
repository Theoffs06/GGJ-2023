using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForRoot : MonoBehaviour
{
   [SerializeField] private RewindGun parent;

    private void Start()
    {
        parent = GetComponentInParent<RewindGun>();

    }
    private void OnTriggerEnter(Collider other)
    {

        if (LayerMask.LayerToName(other.gameObject.layer) == "Root")
        {
            DestructibleRoot newRoot = other.gameObject.GetComponent<DestructibleRoot>();

            if (newRoot && parent)
            {
                parent.RootsInArea.Add(newRoot);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (LayerMask.LayerToName(other.gameObject.layer) == "Root")
        {
            DestructibleRoot newRoot = other.gameObject.GetComponent<DestructibleRoot>();

            if (newRoot && parent)
            {
                newRoot.StopRewind();
                parent.RootsInArea.Remove(newRoot);
            }
        }
    }
}
