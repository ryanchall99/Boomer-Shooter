using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] CinemachineCamera DeathVirtualCamera;
    [SerializeField] TMP_Text ShieldText;
    [SerializeField] Transform WeaponCamera;
    [SerializeField] int MaxHealth;

    int _currentHealth;
    int _gameOverCameraPriority = 20;

    void Start()
    {
        _currentHealth = MaxHealth;

        UpdateShieldText();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        UpdateShieldText();

        if (_currentHealth <= 0)
        {
            WeaponCamera.parent = null;
            DeathVirtualCamera.Priority = _gameOverCameraPriority;
            Destroy(this.gameObject);
        }
    }

    void UpdateShieldText()
    {
        ShieldText.text = ((float)_currentHealth / (float) MaxHealth).ToString("P0");
    }
}
