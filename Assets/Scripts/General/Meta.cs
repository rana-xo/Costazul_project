using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{

      public Transform _player;
      // Para que no se rompa :P

      private void OnTriggerEnter(Collider other) //SI FUNCA
      {
          _player = GameObject.FindGameObjectWithTag("Player").transform;

          if (other.gameObject.tag == "Player");
          {
              SceneManager.LoadSceneAsync(4);
          }
      }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IObjectInteract interactor))
        {
            interactor.Interact();
        }
    }

    //------------------------------------------------------------------

    public bool CanInteract()
    { return true; }

    public bool Interact(Interactor interactor)
    {
        SceneManager.LoadSceneAsync(4);
    }*/
}
