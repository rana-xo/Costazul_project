using UnityEngine;

public class BarrelExplode : MonoBehaviour, IDamagable
{

    [SerializeField] private float _health = 20f;
    float count;
    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
