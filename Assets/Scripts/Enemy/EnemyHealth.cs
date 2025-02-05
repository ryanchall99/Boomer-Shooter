using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;

    int m_CurrentHealth;

    private void Start() 
    {
        m_CurrentHealth = maxHealth;    
    }

    public void TakeDamage(int damage)
    {
        // Reduce Health By Damage Taken
        m_CurrentHealth -= damage;

        if (m_CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }    
}
