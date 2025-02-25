using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO StartingWeapon;
    [SerializeField] CinemachineCamera PlayerFollowCamera;
    
    [Header("Overlay")]
    [SerializeField] GameObject ZoomVignette;
    [SerializeField] TMP_Text AmmoText;

    private PlayerController _playerController;
    private Animator _animator;
    private Weapon _currentWeapon;
    private WeaponSO _currentWeaponSO;
    private float _defaultZoom;
    private float _defaultLookSensitivity;

    InputAction _zoomAction;
    InputAction _fireAction;

    float _timeSinceLastShot = 0f;
    int _currentAmmo;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();

        _defaultZoom = PlayerFollowCamera.Lens.FieldOfView;
        _defaultLookSensitivity = _playerController.GetLookSensitivity();
    }

    void Start()
    {
        _zoomAction = _playerController.GetInput().Player.Zoom;
        _fireAction = _playerController.GetInput().Player.Fire;

        SwitchWeapons(StartingWeapon); // Equips Starting Weapon
        AdjustAmmo(_currentWeaponSO.ClipSize);
    }

    void Update()
    {
        _timeSinceLastShot += Time.deltaTime; // Adding time every frame.

        HandleZoomWeapon();
        HandleShootWeapon();
    }

    public void AdjustAmmo(int amount)
    {
        _currentAmmo += amount;

        AmmoText.text = _currentAmmo.ToString("D2");
    }

    public void HandleShootWeapon()
    {
        // If No Weapon Assigned
        if (!_currentWeapon)
        {
            Debug.LogError("No Weapon Assigned!");
            return;
        }

        if (_fireAction.IsPressed() && _currentWeaponSO.IsAutomatic || _fireAction.WasPressedThisFrame()) // Automatic || Semi Automatic
        {
            if (_timeSinceLastShot >= _currentWeaponSO.FireRate)
            {
                _currentWeapon.Shoot(_currentWeaponSO);
                _animator.Play(AnimationNames.Shoot, 0, 0f); // Animation / Layer / Start Frame
            
                _timeSinceLastShot = 0f; // Reset back to 0
            }
        }
    }

    public void HandleZoomWeapon()
    {
        if (!_currentWeaponSO.CanZoom) return; // Exit early due to weapon not being zoomable.

        if (_zoomAction.IsPressed())
        {
            Zoom(_currentWeaponSO.ZoomAmount, true, _currentWeaponSO.ZoomLookSensitivity);
        }
        else
        {
            Zoom(_defaultZoom, false, _defaultLookSensitivity);
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

        this._currentWeaponSO = weaponSO; // Update weaponSO to new weapons SO
    }

    private void Zoom(float FOV, bool ActiveVignette, float LookSensitivity)
    {
        PlayerFollowCamera.Lens.FieldOfView = FOV;
        ZoomVignette.SetActive(ActiveVignette);
        _playerController.ChangeLookSensitivity(LookSensitivity);
    }
}
