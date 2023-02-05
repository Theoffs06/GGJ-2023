using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindGun : Weapon
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

    [SerializeField]
    public bool CallRewind = false;
    [SerializeField]
    private bool m_IsRewinding = false;

    [SerializeField]
    private float m_RewindTimerMax = 2f;
    [SerializeField]
    private float m_RewindTimer = 0f;

    [SerializeField]
    private float m_CollectTimerMax = 0.5f;
    [SerializeField]
    private float m_CollectTimer = 0f;

    [SerializeField]
    private List<DestructibleRoot> m_RootsInArea = new List<DestructibleRoot>();
    [SerializeField] private List<TimeCrystal> m_CrystalInArea = new List<TimeCrystal>();
    [SerializeField]
    private List<int> m_RootsDestructionQueue = new List<int>();

    private TimeManager m_TimeManager;
    private bool onCooldown = false;

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
        if (Input.GetKey("q"))
        {
            m_CollectTimer += Time.deltaTime;
            if (m_CollectTimer >= m_CollectTimerMax)
            {
                foreach (TimeCrystal crystal in m_CrystalInArea.ToArray())
                {
                    m_CrystalInArea.Remove(crystal);
                    crystal.Collect();

                }
                m_CollectTimer = 0;

            }
        }
        if (Input.GetKeyUp("q"))
        {
            m_CollectTimer = 0;
        }

        if (Input.GetButton("Fire2") )
        {
            if(m_TimeManager.TimeCharge > 0 && !onCooldown)
            {
                CallRewind = true;
                GetComponent<MeshRenderer>().enabled = true;

            }
        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (CallRewind)
            {
                m_RewindTimer = 0;
                onCooldown = true;
            }
            GetComponent<MeshRenderer>().enabled = false;
            CallRewind = false;


        }
        if(onCooldown)
        {
            if (m_RewindTimer <= m_RewindTimerMax)
            {
                m_RewindTimer += Time.deltaTime;
            }
            else
                onCooldown = false;
        }
        if (Input.GetKey("e") && m_TimeManager.TimeCharge > 0)
        {
            RewindRoots();
        }
        
        if (Input.GetKeyUp("e") && m_TimeManager.TimeCharge > 0)
        {
            StopRewindRoots();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RewindEnemies();

    }

    void RewindEnemies()
    {
        if (CallRewind)
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
        if (LayerMask.LayerToName(other.gameObject.layer) == "Crystal")
        {
            TimeCrystal crystal = other.gameObject.GetComponent<TimeCrystal>();
            if (crystal)
            {
                m_CrystalInArea.Add(crystal);
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
        if (LayerMask.LayerToName(other.gameObject.layer) == "Crystal")
        {
            TimeCrystal crystal = other.gameObject.GetComponent<TimeCrystal>();

            if (crystal)
            {
                m_CrystalInArea.Remove(crystal);
            }
        }
    }
}
