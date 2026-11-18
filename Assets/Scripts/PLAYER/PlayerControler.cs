using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.LowLevelPhysics;


interface IObjectInteract
{
    public bool CanInteract();

    public bool Interact(Interactor interactor);
}

public class PlayerControler : MonoBehaviour, IDamagable
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    private float gravityMultiplier = 1.0f;
    private float _velocity = 0f;

    [Header("Health")]
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private float _damageRecieved = 10f;
    [Header("Shoot")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    private Vector2 _input;
    private Vector3 _moveDirection;
    private float _verticalVelocity;
    private float _moveVelocity;
    private CharacterController _playerControler;

    private Animator animator;


    void Awake()
    {
        _playerControler = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();;
    }

    // ------- Movimiento teclado
    void Update()
    {
        Gravity();

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        _moveDirection = new Vector3(_input.x, _moveDirection.y, _input.y);

        _playerControler.Move(_moveDirection * _speed * Time.deltaTime); //delta = ecualizador fps

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.Play("Mushroom_Attack02Smile 0");
            if (AudioManager.Instance != null)
                AudioManager.Instance.AttackSFX();

            Shoot();
        }
    }
    void Gravity()
    {
        
        if (_playerControler.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _moveDirection.y = _velocity;
    }

    public void TakeDamage(float damage)
    {
        // _health = _health - damage; // equivalencia
        _maxHealth -= damage;

        if (_maxHealth < 0)
        {
            SceneManager.LoadSceneAsync(3);
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab.gameObject, _shootPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().Launch(_shootPoint.forward);
        animator.SetBool("Attack", true);
    }
}

