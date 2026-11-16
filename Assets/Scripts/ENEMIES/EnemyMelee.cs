using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : EnemyController, IDamagable
{
    [Header("MeleeParameters")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRadious;


    void Update()
    {
       
    }

    public void AttackCollider()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(attackController.position, attackRadious);

        foreach (Collider2D collider in objetos)
        {
            if (collider.TryGetComponent<IDamagable>(out IDamagable damage))
            {
                Destroy(gameObject);
                _anim.SetBool("Attack", false);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRadious);
    }

   
}

