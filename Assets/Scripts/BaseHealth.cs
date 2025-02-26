using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{
    [SerializeField] int MaxHealth;

    private int _currentHealth;

    void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public abstract void Death();
}
