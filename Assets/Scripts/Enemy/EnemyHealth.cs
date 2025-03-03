using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject ExplosionVFX;
    [SerializeField] int MaxHealth;

    int _currentHealth;

    void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            SelfDestruct();
        }
    }
    public void SelfDestruct()
    {
        // Instantiate VFX Prefab
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
