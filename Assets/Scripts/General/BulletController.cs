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


    // ------ Destruirse al chocar y generar daño
    private void OnCollisionEnter(Collision collision) //kaput enemigo
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damage))
        {
            damage.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
      
        /*
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = collision.gameObject;
            Destroy(enemy);
        }

        Destroy(gameObject);
        */
}
