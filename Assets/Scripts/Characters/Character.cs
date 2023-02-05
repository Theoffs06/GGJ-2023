using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected enum AnimState
    {
        Idle = 0,
        Run
    }

    // Components
    [SerializeField]
    protected Transform m_MeshTransform = null;
    protected Animator m_Animator = null;

    protected AnimState m_CurrentState = AnimState.Idle;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (m_MeshTransform)
        {
            m_Animator = m_MeshTransform.gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
