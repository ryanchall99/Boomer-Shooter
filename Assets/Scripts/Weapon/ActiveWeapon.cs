using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineCamera PlayerFollowCamera;
    [SerializeField] GameObject ZoomVignette;

    private PlayerController _playerController;
    private Animator _animator;
    private Weapon _currentWeapon;
    private float _defaultZoom;
    private float _defaultLookSensitivity;

    InputAction _zoomAction;
    InputAction _fireAction;

    float _timeSinceLastShot = 0f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _playerController = GetComponentInParent<PlayerController>();

        _zoomAction = _playerController.GetInput().Player.Zoom;
        _fireAction = _playerController.GetInput().Player.Fire;

        _defaultZoom = PlayerFollowCamera.Lens.FieldOfView;
        _defaultLookSensitivity = _playerController.GetLookSensitivity();
    }

    void Update()
    {
        _timeSinceLastShot += Time.deltaTime; // Adding time every frame.

        HandleZoomWeapon();
        HandleShootWeapon();
    }

    public void HandleShootWeapon()
    {
        // If No Weapon Assigned
        if (!_currentWeapon)
        {
            Debug.LogError("No Weapon Assigned!");
            return;
        }

        if (_fireAction.IsPressed() && weaponSO.IsAutomatic || _fireAction.WasPressedThisFrame()) // Automatic || Semi Automatic
        {
            if (_timeSinceLastShot >= weaponSO.FireRate)
            {
                _currentWeapon.Shoot(weaponSO);
                _animator.Play(AnimationNames.Shoot, 0, 0f); // Animation / Layer / Start Frame
            
                _timeSinceLastShot = 0f; // Reset back to 0
            }
        }
    }

    public void HandleZoomWeapon()
    {
        if (!weaponSO.CanZoom) return; // Exit early due to weapon not being zoomable.

        if (_zoomAction.IsPressed())
        {
            ToggleZoom(weaponSO.ZoomAmount, true, weaponSO.ZoomLookSensitivity);
        }
        else
        {
            ToggleZoom(_defaultZoom, false, _defaultLookSensitivity);
        }
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

    private void ToggleZoom(float FOV, bool IsVignetteActive, float LookSensitivity)
    {
        PlayerFollowCamera.Lens.FieldOfView = FOV;
        ZoomVignette.SetActive(IsVignetteActive);
        _playerController.ChangeLookSensitivity(LookSensitivity);
    }
}
