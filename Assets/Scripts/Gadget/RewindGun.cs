using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class RewindGun : Weapon
{
    enum RewindMode
    {
        Enemy = 0,
        Root
    }

    // Components
    [SerializeField]
    MeshRenderer m_MeshRenderer = null;

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
    public List<DestructibleRoot> RootsInArea = new List<DestructibleRoot>();
    [SerializeField] private List<TimeCrystal> m_CrystalInArea = new List<TimeCrystal>();
    [SerializeField]
    private List<int> m_RootsDestructionQueue = new List<int>();

    [Header("Audio")] 
    [SerializeField] private StudioEventEmitter actionEvent;

    private PlayerCharacter m_Player;

    private TimeManager m_TimeManager;
    private bool onCooldown = false;

    [SerializeField]
    private GameObject m_MeshObject;

    // Targeting mode
    RewindMode m_CurrentMode = RewindMode.Enemy;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<PlayerCharacter>();
        m_RewindManager = FindObjectOfType<RewindManager>();
        m_TimeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(m_TimeManager.TimeCharge > 0)
        {
            if (Input.GetButton("Fire2") && m_Player.Life >0)
            {
                if (m_TimeManager.TimeCharge > 0 && !onCooldown)
                {
                    CallRewind = true;
                    Show(true);
                    if (!actionEvent.IsPlaying())
                    {
                        actionEvent.Play();
                    }
                }
            }
        }
        else
        {
            if (CallRewind)
            {
                m_RewindTimer = 0;
                onCooldown = true;
            }
            Show(false);
            foreach (Creature creature in FindObjectsOfType<Creature>())
            {
                if (creature.GetComponent<FilteredRewind>().Selected)
                    creature.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            CallRewind = false;

        }
        if (Input.GetButtonUp("Fire2") && m_Player.Life > 0)
        {
            if (CallRewind)
            {
                m_RewindTimer = 0;
                onCooldown = true;
            }
            Show(false);
            foreach (Creature creature in FindObjectsOfType<Creature>())
            {
                if (creature.GetComponent<FilteredRewind>().Selected)
                    creature.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            CallRewind = false;


        }
        if (onCooldown)
        {
            if (m_RewindTimer <= m_RewindTimerMax)
            {
                m_RewindTimer += Time.deltaTime;

            }
            else
                onCooldown = false;
        }
        if (Input.GetKey("e") && m_TimeManager.TimeCharge > 0 && m_Player.Life > 0 && !m_Player.WinScreen.activeSelf)
        {
            RewindRoots();
            Show(true);
        }

        if (Input.GetKeyUp("e") && m_TimeManager.TimeCharge > 0 && m_Player.Life > 0 && !m_Player.WinScreen.activeSelf)
        {
            StopRewindRoots();
            Show(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RewindEnemies();

    }

    public bool IsVisible()
    {
        if (m_MeshObject)
        {
            return m_MeshObject.activeSelf;
        }

        return false;
    }

    public void Show(bool show)
    {
        if (m_MeshObject)
        {
            m_MeshObject.SetActive(show);
        }
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
        int numRoots = RootsInArea.Count;
        for (int i = 0; i < numRoots; i++)
        {
            if (RootsInArea[i])
            {
                RootsInArea[i].Rewind();
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
            RootsInArea.RemoveAt(indexToRemove);
        }

        m_RootsDestructionQueue.Clear();
    }

    void StopRewindRoots()
    {
        m_TimeManager.IsRewinding = false;
        int numRoots = RootsInArea.Count;
        for (int i = 0; i < numRoots; i++)
        {
            if (RootsInArea[i])
            {
                RootsInArea[i].StopRewind();
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

        /*if (LayerMask.LayerToName(other.gameObject.layer) == "Root")
        {
            DestructibleRoot newRoot = other.gameObject.GetComponent<DestructibleRoot>();

            if (newRoot)
            {
                RootsInArea.Add(newRoot);
            }
        }*/

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

        /*if (LayerMask.LayerToName(other.gameObject.layer) == "Root")
        {
            DestructibleRoot newRoot = other.gameObject.GetComponent<DestructibleRoot>();

            if (newRoot)
            {
                newRoot.StopRewind();
                RootsInArea.Remove(newRoot);
            }
        }*/
    }
}
