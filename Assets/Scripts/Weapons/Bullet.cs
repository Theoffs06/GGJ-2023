using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    // Components
    Rigidbody m_RigidBody = null;

    [SerializeField]
    private float m_Lifespan = 2f;

    [SerializeField]
    private float m_Speed = 20f;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Lifespan -= Time.deltaTime;

        if (m_Lifespan <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (m_RigidBody)
        {
            Vector3 newPosition = transform.position + transform.forward * m_Speed * Time.fixedDeltaTime;

            m_RigidBody.MovePosition(newPosition);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string layerName = other.gameObject.name;
        Debug.Log(layerName);

        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        {
            // TODO : Implement take damage.
        }

        Destroy(gameObject);
    }
}
