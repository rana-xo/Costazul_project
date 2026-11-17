using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyRange : EnemyController
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    [Header("Range")]
    [SerializeField] private float _shootRange = 8f;        
    [SerializeField] private float _turnSpeed = 8f;

    private float timer;

    // ------ Calcular tiempo entre disparos y disparar
    protected void Update()
    {
        timer += Time.deltaTime;

        if (_distance > _shootRange)
        {
            timer = 0f; // fuera de rango: no dispara
            return;
        }

        FacePlayer();

        if (timer > 2)
        {
            _anim.SetBool("Idle", true);
            timer = 0;
            Shoot();

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.NPCShootSFX();
            }
        }
    }

    // ------ Mirar hacia el Jugador 
    void FacePlayer()
    {
        Vector3 toPlayer = _player.position - transform.position;
        toPlayer.y = 0f;

        if (toPlayer.sqrMagnitude < 0.0001f)
            return;

        Quaternion look = Quaternion.LookRotation(toPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, _turnSpeed * Time.deltaTime);
    }

    // ------ Disparar (generar bala)
    void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab.gameObject, _shootPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().Launch(_shootPoint.forward);
        _anim.SetBool("Attack", true);
    }
}
