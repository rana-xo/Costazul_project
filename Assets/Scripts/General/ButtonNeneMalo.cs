using UnityEngine;

public class ButtonNeneMalo : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;

    public Transform _player;

    // Update is called once per frame
  
    private void OnTriggerEnter(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (other.gameObject.tag == "Player");
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab.gameObject, _shootPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletController>().Launch(_shootPoint.forward);
    }
}
