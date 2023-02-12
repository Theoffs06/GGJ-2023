using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DestructibleRoot : MonoBehaviour
{
    [SerializeField]
    private float m_DestructionTime = 5f;

    [SerializeField]
    private float m_ChangeRespawnRate = 1.5f;
    [SerializeField]
    private float m_BaseRespawnRate = 6f;

    private float m_RemainingTime = 0f;
    [SerializeField]
    private Spawner[] m_Spawners;
    [SerializeField]
    private float m_RegenTimeScale = 2f;

    private bool m_IsRewinding = false;

    private float m_CurrentScaleRatio = 1f;
    private Vector3 m_StartScale;
    [SerializeField]
    private GameObject winScreen;

    [Header("Audio")] 
    [SerializeField] private StudioEventEmitter deathEvent;
    [SerializeField] private StudioEventEmitter musicEven; 


    // Start is called before the first frame update
    void Start()
    {
        m_RemainingTime = m_DestructionTime;
        m_StartScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsRewinding)
        {
            Regenerate();
        }
    }

    public void Rewind()
    {
        foreach (Spawner spawner in m_Spawners)
            spawner.spawnRate = m_ChangeRespawnRate;
        m_IsRewinding = true;
        m_RemainingTime -= Time.deltaTime;
        UpdateScale();
    }

    public void StopRewind()
    {
        foreach (Spawner spawner in m_Spawners)
            spawner.spawnRate = m_BaseRespawnRate;

        m_IsRewinding = false;
    }

    public void Regenerate()
    {
        if (m_RemainingTime < m_DestructionTime)
        {
            m_RemainingTime += Time.deltaTime / m_RegenTimeScale;
            m_RemainingTime = Mathf.Min(m_RemainingTime, m_DestructionTime);
            UpdateScale();
        }
    }

    void UpdateScale()
    {
        m_CurrentScaleRatio = m_RemainingTime / m_DestructionTime;
        m_CurrentScaleRatio = Mathf.Max(m_CurrentScaleRatio, 0f);

        transform.localScale = m_StartScale * m_CurrentScaleRatio;

        if (m_RemainingTime <= 0f)
        {
            winScreen.SetActive(true);
            musicEven.Stop();
            deathEvent.Play();
            Destroy(gameObject);

        }
    }
}
