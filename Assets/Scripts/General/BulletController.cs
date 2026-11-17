using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //Unity revisa que haya rigidbody y avisa si falta
public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damage = 10f;
    private Rigidbody _rb;

    // ------ "backup" para que "castee" un rigidbody si no existe
    void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
    }
    
    // ------ Desactivarse 
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // ------ Movimiento
    public void Launch(Vector3 direction)
    {
        _rb.linearVelocity = direction.normalized * _speed * Time.fixedDeltaTime;
        //normalized = 1 aka que no vaya mas rapido de lo que debe
    }

    // ------- Generar daño
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damage = other.GetComponentInParent<IDamagable>();
        if (damage != null)
        {
            damage.TakeDamage(_damage);
            AudioManager.Instance.PlayerDMG();
            Destroy(gameObject);
        }
    }

    // ------ Generar daño x2, porque no logro resolver cuál es el que funciona con enemies y player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damage))
        {
            damage.TakeDamage(_damage);
            AudioManager.Instance.PlayerDMG();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
