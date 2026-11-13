using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{

    public Transform _player;
    // Para que no se rompa :P

    private void OnTriggerEnter(Collider other)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (other.gameObject.tag == "Player") ;
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
}
