using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class PlayerCharacter : Character
{
    // Components
    private CharacterController m_CharacterController = null;

    // Player life
    [SerializeField]
    public int HP = 100;
    public int Life = 3;


    // Player's moving speed
    [SerializeField]
    private float m_MoveSpeed = 10f;

    private Vector2 m_MoveAxis = Vector2.zero;

    // Array of weapons
    [SerializeField]
    private List<Weapon> m_WeaponList;
    private int m_NumWeapons = 0;
    [SerializeField]
    private Weapon rewindGun;

    // Current weapon index
    private int m_CurrentWeaponIndex = 0;

    private bool m_GamepadMode = false;
    [SerializeField]
    private GameObject m_Menu;

    [Header("Audio")] 
    public StudioEventEmitter damageEvent;
    [SerializeField] private StudioEventEmitter brokenLifeEvent; 
    [SerializeField] private StudioEventEmitter deathEvent;

    private bool playDeath;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_CharacterController = GetComponent<CharacterController>();

        InitializeWeapons();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        UpdateMouseMoveAxis();
        UpdateInputMoveAxis();
        UpdateMouseShoot();
        UpdateJoystickShootMoveAxis();

        if(HP <= 0)
        {
            brokenLifeEvent.Play();
            Life--;
            HP = 100;
        }
        if (Input.GetKeyDown("f"))
            CycleWeapon();

        if (rewindGun.GetComponent<MeshRenderer>().enabled)
            m_WeaponList[m_CurrentWeaponIndex].GetComponent<MeshRenderer>().enabled = false;
        else
            m_WeaponList[m_CurrentWeaponIndex].GetComponent<MeshRenderer>().enabled = true;

        if (Input.GetKeyDown("escape"))
        {
            m_Menu.GetComponent<Menu>().Pause();

        }


        if (Life <= 0) {
            if (!deathEvent.IsPlaying() && !playDeath) {
                playDeath = true;
                deathEvent.Play();
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = m_MoveAxis.x * m_MoveSpeed;
        velocity.z = m_MoveAxis.y * m_MoveSpeed;

        if (m_MeshTransform && velocity.sqrMagnitude > 0f)
        {
            m_MeshTransform.rotation = Quaternion.LookRotation(velocity.normalized);
        }

        if (m_Animator)
        {
            m_Animator.SetFloat("Speed", velocity.magnitude);
        }

        if (m_CharacterController)
        {
            m_CharacterController.SimpleMove(velocity);
        }
    }

    void InitializeWeapons()
    {
        m_NumWeapons = m_WeaponList.Count;

        for (int i = 0;i < m_NumWeapons; i++)
        {
            m_WeaponList[i] = Instantiate<Weapon>(m_WeaponList[i], transform);
            m_WeaponList[i].SetWeaponRotation(transform.forward);
        }
    }

    void UpdateInputMoveAxis()
    {
        m_MoveAxis.x = Input.GetAxis("Horizontal");
        m_MoveAxis.y = Input.GetAxis("Vertical");

        m_MoveAxis.Normalize();
    }

    void UpdateMouseMoveAxis()
    {
        if (!m_GamepadMode)
        {
            Vector2 ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 MousePosition = Input.mousePosition;

            Vector2 direction = (ScreenPosition - MousePosition).normalized;
            direction.x *= -1;

            m_WeaponList[m_CurrentWeaponIndex].SetWeaponRotation(direction);
            rewindGun.SetWeaponRotation(direction);
        }
    }

    void UpdateMouseShoot()
    {
        if (Input.GetButton("Fire1"))
        {
            m_GamepadMode = false;
            FireWeapon();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopWeapon();
        }
    }

    void UpdateJoystickShootMoveAxis()
    {
        Assert.IsTrue(m_CurrentWeaponIndex >= 0 && m_CurrentWeaponIndex < m_WeaponList.Count);

        Vector2 stickDirection = Vector2.zero;
        stickDirection.x = Input.GetAxis("Right Joystick X");
        stickDirection.y = Input.GetAxis("Right Joystick Y");

        stickDirection.Normalize();

        if (stickDirection.sqrMagnitude > 0)
        {
            m_GamepadMode = true;
            if (m_CurrentWeaponIndex >= 0 && m_CurrentWeaponIndex < m_WeaponList.Count)
            {
                m_WeaponList[m_CurrentWeaponIndex].SetWeaponRotation(stickDirection);
            }
            rewindGun.SetWeaponRotation(stickDirection);

            FireWeapon();
        }
        else if (m_GamepadMode)
        {
            StopWeapon();
        }
    }

    void FireWeapon()
    {
        if (m_CurrentWeaponIndex >= 0 && m_CurrentWeaponIndex < m_WeaponList.Count)
        {
            m_WeaponList[m_CurrentWeaponIndex].StartShooting();
        }
    }

    void StopWeapon()
    {
        if (m_CurrentWeaponIndex >= 0 && m_CurrentWeaponIndex < m_WeaponList.Count)
        {
            m_WeaponList[m_CurrentWeaponIndex].StopShooting();
        }
    }

    void CycleWeapon()
    {
        if (m_CurrentWeaponIndex >= 0 && m_CurrentWeaponIndex < m_WeaponList.Count)
        {

            m_WeaponList[m_CurrentWeaponIndex].StopShooting();
            m_WeaponList[m_CurrentWeaponIndex].gameObject.SetActive(false);
            if (m_CurrentWeaponIndex == m_NumWeapons - 1)
            {
                m_CurrentWeaponIndex = 0;
            }
            else
            {
                m_CurrentWeaponIndex++;
            }
            m_WeaponList[m_CurrentWeaponIndex].gameObject.SetActive(true);

        }
    }
}
