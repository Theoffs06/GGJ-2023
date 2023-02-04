using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform m_TargetTransform = null;

    [SerializeField]
    [Range(0f, 50f)]
    private float m_Distance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter TargetPlayer = FindObjectOfType<PlayerCharacter>();
        
        if (TargetPlayer)
        {
            m_TargetTransform = TargetPlayer.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TargetTransform)
        {
            transform.position = m_TargetTransform.position + Vector3.up * m_Distance;
        }
    }
}
