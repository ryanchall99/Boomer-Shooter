using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    private Animator _animator;
    Weapon _currentWeapon;

    float _timeSinceLastShot = 0f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        _timeSinceLastShot += Time.deltaTime; // Adding time every frame.
    }

    public void HandleShoot()
    {
        if (_timeSinceLastShot >= weaponSO.FireRate)
        {
            _currentWeapon.Shoot(weaponSO);
            _animator.Play(AnimationNames.Shoot, 0, 0f); // Animation / Layer / Start Frame
            
            _timeSinceLastShot = 0f; // Reset back to 0
        }
    }

    public bool IsAutomatic()
    {
        return weaponSO.IsAutomatic;
    }
}
