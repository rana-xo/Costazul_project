using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour, IDamagable
{

    [Header("Stats")]
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private float _detectionRange = 10f;
    [SerializeField] protected float _attackRange = 2f;
    [SerializeField] protected float _attackCooldown = 1.2f;
    [SerializeField] protected float _damage = 10f;

    [Header("Death")] //Nota: integrar animaciones :P

    [SerializeField] private float sinkSpeed = 1f;
    [SerializeField] private float sinkDuration = 1.5f;

    [Header("Chase")]
    public Transform _player;
    public float AttackDistance;

    // ------ cosas del inspector :o
    public NavMeshAgent _navmesh;
    public Animator _anim;
    public float _distance;
    private Rigidbody _rb;

    // ------ navmesh, anim, player
    // ------ Error messages
    private void Start()
    {
        _navmesh = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_player == null)
            Debug.LogError("Player not found in scene. Add tag 'Player' to it. Mamahuevo");

        _navmesh = GetComponent<NavMeshAgent>();
        if (_navmesh == null)
            Debug.LogError("Player not found in " + this.name + " Add NavMeshAgent to it. Vos podés wachín");

        _anim = GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("Player not found in " + this.name + " Add animator to it. six seven ahre");
    }

    // -------- Chase state

    protected virtual void Update()
    {
        _distance = Vector3.Distance(_navmesh.transform.position, _player.position);


        if (_distance < AttackDistance)
        {
            _navmesh.isStopped = true;
            _anim.SetBool("Attack", true);
        }
        else
        {
            _navmesh.isStopped = false;
            _anim.SetBool("Attack", false);
            _navmesh.destination = _player.position; 
        }


        if (_navmesh.velocity.magnitude != 0f)
        {
            _anim.SetBool("Chase", true);
        }
        else
        {
            _anim.SetBool("Chase", false);
        }
    }

    // ------- rango en el que toma el daño
    protected virtual float AttackReach
    {
        get
        {
            return 1.5f;
        }
    }

    // ------ Moverse usando navmesh
    void OnNavMeshMove()
    {
        if (_anim.GetBool("Chase") == true)
        {
            _navmesh.speed = (_anim.deltaPosition / Time.deltaTime).magnitude;
            
        }
    }

    // ------- Destruir enemigo
    public void TakeDamage(float damage)
    {
        _maxHealth -= damage;

        if (_maxHealth <+ 0)
        {
            Destroy(gameObject);
            AudioManager.Instance.NpcDMG();
        }
    }
}
