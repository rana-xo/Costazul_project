using System.Threading;
using UnityEngine;

public class EnemyRange : EnemyController
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
        {
            _anim.SetBool("Idle", true);
            timer = 0;
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
