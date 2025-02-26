using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject ExplosionVFX;
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
            Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }   
}
