using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerHealth playerHealth;
    void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    /* private void OnEnable()
     {
         if(playerHealth !=null)
         {
             playerHealth.OnHealthChanged += UpdateHealthUI;
             UpdateHealthUI(playerHealth.CurrentHealth, )
         }
     } */
}
