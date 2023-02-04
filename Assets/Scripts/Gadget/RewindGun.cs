using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindGun : MonoBehaviour
{
    enum RewindMode
    {
        Enemy = 0,
        Root
    }

    [SerializeField]
    private float m_RewindIntensity = 0.02f;

    private float m_RewindValue = 0f;

    private RewindManager m_RewindManager = null;

    private bool m_CallRewind = false;
    private bool m_IsRewinding = false;

    [SerializeField]
    private float m_RewindTimer = 3f;

    private List<DestructibleRoot> m_RootsInArea = new List<DestructibleRoot>();
    private List<int> m_RootsDestructionQueue = new List<int>();

    private TimeManager m_TimeManager;

    // Targeting mode
    RewindMode m_CurrentMode = RewindMode.Enemy;

    // Start is called before the first frame update
    void Start()
    {
        m_RewindManager = FindObjectOfType<RewindManager>();
        m_TimeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire2") && m_TimeManager.TimeCharge > 0)
        {
            m_CallRewind = true;
        }
        else
        {
            m_CallRewind = false;
        }

        if (Input.GetButton("Fire3") && m_TimeManager.TimeCharge > 0)
        {
            RewindRoots();
        }
        
        if (Input.GetButtonUp("Fire3") && m_TimeManager.TimeCharge > 0)
        {
            StopRewindRoots();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_CurrentMode == RewindMode.Enemy)
        {
            RewindEnemies();
        }
    }

    void RewindEnemies()
    {
        if (m_CallRewind)
        {
            if (!m_IsRewinding)
            {
                // Start rewind
                m_RewindManager.StartRewindTimeBySeconds(m_RewindIntensity);

                m_IsRewinding = true;
                m_TimeManager.IsRewinding = true;
            }
            else
            {
                m_RewindValue += m_RewindIntensity;

                if (m_RewindManager.HowManySecondsAvailableForRewind > m_RewindValue)
                {
                    m_RewindManager.SetTimeSecondsInRewind(m_RewindValue);
                }
            }
        }
        else if (m_IsRewinding)
        {
            m_RewindManager.StopRewindTimeBySeconds();
            m_IsRewinding = false;
            m_TimeManager.IsRewinding = false;

            m_RewindValue = 0f;
        }
        
    }

    void RewindRoots()
    {
        m_TimeManager.IsRewinding = true;
        int numRoots = m_RootsInArea.Count;
        for (int i = 0; i < numRoots; i++)
        {
            if (m_RootsInArea[i])
            {
                m_RootsInArea[i].Rewind();
            }
            else
            {
                m_RootsDestructionQueue.Add(i);
            }
        }

        int QueueLength = m_RootsDestructionQueue.Count;
        for (int i = QueueLength - 1; i >= 0; i--)
        {
            int indexToRemove = m_RootsDestructionQueue[i];
            m_RootsInArea.RemoveAt(indexToRemove);
        }

        m_RootsDestructionQueue.Clear();
    }

    void StopRewindRoots()
    {
        m_TimeManager.IsRewinding = false;
        int numRoots = m_RootsInArea.Count;
        for (int i = 0; i < numRoots; i++)
        {
            if (m_RootsInArea[i])
            {
                m_RootsInArea[i].StopRewind();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        {
            FilteredRewind rewindComponent = other.gameObject.GetComponent<FilteredRewind>();

            if (rewindComponent)
            {
                rewindComponent.Selected = true;
            }
        }

        if (LayerMask.LayerToName(other.gameObject.layer) == "Root")
        {
            DestructibleRoot newRoot = other.gameObject.GetComponent<DestructibleRoot>();

            if (newRoot)
            {
                m_RootsInArea.Add(newRoot);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        {
            FilteredRewind rewindComponent = other.gameObject.GetComponent<FilteredRewind>();

            if (rewindComponent)
            {
                rewindComponent.Selected = false;
            }
        }

        if (LayerMask.LayerToName(other.gameObject.layer) == "Root")
        {
            DestructibleRoot newRoot = other.gameObject.GetComponent<DestructibleRoot>();

            if (newRoot)
            {
                newRoot.StopRewind();
                m_RootsInArea.Remove(newRoot);
            }
        }
    }
}
