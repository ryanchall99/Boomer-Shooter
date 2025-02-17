using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    private Animator _animator;
    private Weapon _currentWeapon;

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

    public void ShootWeapon()
    {
        // If No Weapon Assigned
        if (!_currentWeapon)
        {
            Debug.LogError("No Weapon Assigned!");
            return;
        }

        if (_timeSinceLastShot >= weaponSO.FireRate)
        {
            _currentWeapon.Shoot(weaponSO);
            _animator.Play(AnimationNames.Shoot, 0, 0f); // Animation / Layer / Start Frame
            
            _timeSinceLastShot = 0f; // Reset back to 0
        }
    }

    public void ZoomWeapon()
    {
        if (!weaponSO.CanZoom) return; // Exit early due to weapon not being zoomable.

        Debug.Log("Zooming In");
    }

    public void SwitchWeapons(WeaponSO weaponSO)
    {
        if (_currentWeapon)
        {
            // Destroy current active weapon before switching
            Destroy(_currentWeapon.gameObject);
        }

        if (weaponSO.WeaponPrefab)
        {
            Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>(); // Instantiate new weapon prefab linked to weaponSO
            _currentWeapon = newWeapon; // Update current weapon
        }
        else
        {
            Debug.LogError("No Weapon Prefab Assigned!");
        }

        this.weaponSO = weaponSO; // Update weaponSO to new weapons SO
    }

    public bool IsAutomatic()
    {
        return weaponSO.IsAutomatic;
    }
}
