using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHealth;

    int _CurrentHealth;

    private void Start() 
    {
        _CurrentHealth = MaxHealth;    
    }

    public void TakeDamage(int damage)
    {
        // Reduce Health By Damage Taken
        _CurrentHealth -= damage;

        if (_CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }    
}
