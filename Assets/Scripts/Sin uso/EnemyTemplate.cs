using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI; //navmeshagent es de esta directriz //protected = mantiene valores

// DESCONTINUADO 


public abstract class EnemyTemplate : MonoBehaviour, IDamagable
{

    // ------ Internal Data

    [Header("References")]
    [SerializeField] Transform _player;
    protected NavMeshAgent _agent;
    protected Animator _animator;
    // no necesita serialized porque no se edita?

    [Header("Stats")]
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private float _detectionRange = 10f;
    [SerializeField] protected float _attackRange = 2f;
    [SerializeField] private float _attackCooldown = 1.2f;
    [SerializeField] private float _damage = 10f;

    [Header ("Death")]

    [SerializeField] private float sinkSpeed = 1f; //hundirse al kaput
    [SerializeField] private float sinkDuration = 1.5f; // kaput 100% no colliders infinitos en el mapa

    private float _currentHealth;
    private float _nextAttackTime;
    private bool _deathSequenceStartetd;

    protected EnemyState _currentState { get; private set; } = EnemyState.Idle;
   
    // ------ Avisos de error

    protected void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_player == null)
            Debug.LogError("Player not found in scene. Add tag 'Player' to it. Mamahuevo");

        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
            Debug.LogError("Player not found in "+ this.name +" Add NavMeshAgent to it. Vos podés wachín");

        _animator = GetComponent<Animator>();
        if (_animator == null)
            Debug.LogError("Player not found in " + this.name + " Add animator to it. six seven ahre");
    }

    // ------ Acciones enemigo + Animaciones

    void Update()
    {
        if (_currentState == EnemyState.Death || _player == null)
            return;

        switch (_currentState) //puede ser 0 en vez de currtState
        {
            case EnemyState.Idle:
                UpdateIdleState();
                _animator.SetTrigger("Idle");
                break;

            case EnemyState.Chase:
                UpdateChaseState();
                _animator.SetTrigger("Chase");
                break;

            case EnemyState.Attack:
                UpdateAttackState();
                _animator.SetTrigger("Attack");
                break;

           /* case EnemyState.Death():
                UpdateDeathState();
                break;*/
        }
    }

    //     -----  variables enemy
    private void UpdateDeathState()
    {
        Destroy(gameObject);
    }
    private void UpdateAttackState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if(distanceToPlayer < _attackRange)
        {
            _currentState = EnemyState.Attack;
            _agent.isStopped = true;
        }

        if (_animator.GetBool("Attack") == false)
        {
            _agent.speed = (_animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }
    //------ Generar el daño
    // porque no me tomaba el Collision en "UpdateAttackSTate":P

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);
        }

    }

    //     ----- Movimiento
    //  mueve al detectar player, para al atacar
    private void UpdateChaseState() 
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer > _detectionRange)
        {
            _currentState = EnemyState.Idle;
            _agent.isStopped = true;
        }
        else
        {
            if (distanceToPlayer <= 10f)
            {
                _currentState = EnemyState.Attack;
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
                _agent.destination = _player.position;
            }
        }
    }

    // ------ Idle
    private void UpdateIdleState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _detectionRange)
        {
            _currentState = EnemyState.Chase;
        }
        else
        {
            _currentState = EnemyState.Idle;
        }
    }

    // ------- Daño
    public void TakeDamage(float damage)
    {
        // _health = _health - damage; // equivalencia

        _maxHealth -= damage;

        if (_maxHealth < 0)
        {
            Destroy(gameObject);
        }


        /* VERSION 2
       public void TakeDamage(float damage)
   {
       _health -= damage;
       if(_health <= 0)
       {
           _currectState = EnemyState.Death;
       }
   } */
    }
}
