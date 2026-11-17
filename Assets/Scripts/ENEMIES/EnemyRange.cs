using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyRange : EnemyController
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    private float timer;
    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectsWithTag("Audio").GameObject.GetComponent<AudioManager>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            _anim.SetBool("Idle", true);
            timer = 0;
            AudioManager.Instance.ShootSFX();
            Shoot();
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab.gameObject, _shootPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().Launch(_shootPoint.forward);
        _anim.SetBool("Attack", true);
    }
}
