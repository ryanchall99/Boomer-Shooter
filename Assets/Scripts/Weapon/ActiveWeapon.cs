using Unity.Cinemachine;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineCamera PlayerFollowCamera;

    private Animator _animator;
    private Weapon _currentWeapon;
    private float _defaultZoom;

    float _timeSinceLastShot = 0f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentWeapon = GetComponentInChildren<Weapon>();

        _defaultZoom = PlayerFollowCamera.Lens.FieldOfView;
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

    public void ZoomInWeapon()
    {
        if (!weaponSO.CanZoom) return; // Exit early due to weapon not being zoomable.

        PlayerFollowCamera.Lens.FieldOfView = weaponSO.ZoomAmount;
    }

    public void ZoomOutWeapon()
    {
        if (!weaponSO.CanZoom) return;

        PlayerFollowCamera.Lens.FieldOfView = _defaultZoom;
    }

    public void SwitchWeapons(WeaponSO weaponSO)
    {
        if (!weaponSO.WeaponPrefab)
        {
            Debug.LogError("No Weapon Prefab Assigned!");
            return;
        }

        if (_currentWeapon)
        {
            // Destroy current active weapon before switching
            Destroy(_currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>(); // Instantiate new weapon prefab linked to weaponSO
        _currentWeapon = newWeapon; // Update current weapon

        this.weaponSO = weaponSO; // Update weaponSO to new weapons SO
    }

    public bool IsAutomatic()
    {
        return weaponSO.IsAutomatic;
    }
}
