using UnityEngine;

public interface IDamagable //defino qué cosas deben recibir daño, para no asignarlo a cada script (ej BulletControler)
{
    // I al inicio = interfaz (convención)
    void TakeDamage(float damage);
}
