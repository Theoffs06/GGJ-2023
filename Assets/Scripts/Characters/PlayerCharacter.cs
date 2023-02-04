using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Components
    private CharacterController m_CharacterController = null;

    // Player life
    [SerializeField]
    private int m_HP = 100;

    // Player's moving speed
    [SerializeField]
    private float m_MoveSpeed = 10f;

    private Vector2 m_MoveAxis = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveAxis.x = Input.GetAxis("Horizontal");
        m_MoveAxis.y = Input.GetAxis("Vertical");

        m_MoveAxis.Normalize();
    }

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = m_MoveAxis.x * m_MoveSpeed;
        velocity.z = m_MoveAxis.y * m_MoveSpeed;

        if (m_CharacterController)
        {
            m_CharacterController.SimpleMove(velocity);
        }
    }
}
