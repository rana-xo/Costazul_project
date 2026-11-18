using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyController, IDamagable
{
    [Header("Controller")]
    [SerializeField] private Transform attackController;

    // ------ Realizar ataque
    protected override void Update()
    {

        base.Update();

        if (_distance < AttackReach && Time.time >= _attackCooldown)
        {
            AttackCollider();
        }
    }

    // ------ Generar collider de ataque
    public void AttackCollider()
    {
        Vector3 center = attackController != null ? attackController.position : transform.position;

        Collider[] objetos = Physics.OverlapSphere(center, Mathf.Max(_attackRange, AttackReach));

        foreach (Collider collider in objetos)
        {
            if (collider.GetComponentInParent<EnemyController>() != null)
                continue;

            if (collider.TryGetComponent<IDamagable>(out IDamagable damage) ||
                (collider.attachedRigidbody != null && collider.attachedRigidbody.TryGetComponent(out damage)))
            {
                damage.TakeDamage(_damage);
                AudioManager.Instance.AttackSFX();

                _anim.Play("Cactus_Attack01"); 
                break;                      
            }
        }
        
    }

    // ------ Crear radio de ataque (rojo en el editor)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
      
        Gizmos.DrawWireSphere(attackController.position, _attackRange);
    }

   
}

