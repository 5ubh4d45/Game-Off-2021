using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.HealthSystem;


//this class will contain all the functionalities
public class Player : HealthObject<HeartHealthSystem>
{
    private static Player _instance;
    public static Player Instance => _instance;


    [Header("CameraShake")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float camShakeIntensity = 1f;
    [SerializeField] private float camShakeFrequency = 3f;
    [SerializeField] private float camShakeTime = 0.2f;

    [Space]
    [SerializeField] private float moveSpeed = 10;

    private PlayerCombat _playerCombat;
    private PlayerAnimator _playerAnimator;
    private PlayerMovement _playerMovement;

    private float _meleeDamageCoolDown = 1f;
    private float _nextMeleeDamageTime = 0f;

    public float MoveSpeed => moveSpeed;
    public IInteractable Interactable { get; set; }
    public PlayerCombat PlayerCombat => _playerCombat;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public PlayerMovement PlayerMovement => _playerMovement;


    private void Start()
    {
        _instance = this;
        _playerCombat = GetComponent<PlayerCombat>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }

        _nextMeleeDamageTime += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (_nextMeleeDamageTime >= _meleeDamageCoolDown)
        {
            if (collision2D.gameObject.CompareTag("Enemy"))
            {
                _healthSystem.Damage(1);
                
                _nextMeleeDamageTime = 0f;
                
                //adds cam shake when damage taken
                cameraShake.ShakeCamera(camShakeIntensity, camShakeFrequency, camShakeTime);
            }
            if (collision2D.gameObject.CompareTag("Boss"))
            {
                _healthSystem.Damage(1);
                
                _nextMeleeDamageTime = 0f;
                
                //adds cam shake when damage taken
                cameraShake.ShakeCamera(camShakeIntensity, camShakeFrequency, camShakeTime);
            }
            
        }
        

        

    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Health Point"))
        {
            _healthSystem.Heal(1);

        }
    }
}
