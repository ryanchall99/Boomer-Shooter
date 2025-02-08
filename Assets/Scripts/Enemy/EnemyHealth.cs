using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHealth;

    int _currentHealth;

    private void Start() 
    {
        _currentHealth = MaxHealth;    
    }

    public void TakeDamage(int damage)
    {
        // Reduce Health By Damage Taken
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }    
}
