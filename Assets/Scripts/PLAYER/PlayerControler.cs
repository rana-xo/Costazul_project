using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour, IDamagable
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [Header("Health")]
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private float _damageRecieved = 10f;

    [Header("Other")]
    //quedó para que no se me buguee más abajo
    public Transform _player;
    public GameObject WinScene;

    private Vector2 _input;
    private Vector3 _moveDirection;
    private float _verticalVelocity;
    private float _moveVelocity;
    private CharacterController _characterController;


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // ------- Movimiento teclado
    void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        _moveDirection = new Vector3(_input.x, 0f, _input.y); //mov adelnte atras izq der

        _characterController.Move(_moveDirection * _speed * Time.deltaTime); //delta = ecualizador fps
    }

   /* private void OnTriggerEnter(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (other.gameObject.tag == "Player") ;
        {
            TakeDamage(_damageRecieved);
        }
    }*/

    public void TakeDamage(float damage)
    {
        // _health = _health - damage; // equivalencia
        _maxHealth -= damage;

        if (_maxHealth < 0)
        {
            Destroy(gameObject);
            SceneManager.LoadSceneAsync(3);
        }

    }

    //------- Meta

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            WinScene.gameObject.SetActive(true);
        }
    }
}

