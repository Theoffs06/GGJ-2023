using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DestructibleRoot : MonoBehaviour
{
    [SerializeField]
    private float m_DestructionTime = 5f;
    private float m_RemainingTime = 0f;

    [SerializeField]
    private float m_RegenTimeScale = 2f;

    private bool m_IsRewinding = false;

    private float m_CurrentScaleRatio = 1f;
    private Vector3 m_StartScale;

    [Header("Audio")] 
    private StudioEventEmitter deathEvent;

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
        m_IsRewinding = true;
        m_RemainingTime -= Time.deltaTime;
        UpdateScale();
    }

    public void StopRewind()
    {
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
            deathEvent.Play();
            Destroy(gameObject);
        }
    }
}
