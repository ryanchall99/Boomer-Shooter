using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    private Animator _animator;
    Weapon _currentWeapon;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentWeapon = GetComponentInChildren<Weapon>();
    }


    public void HandleShoot()
    {
        _currentWeapon.Shoot(weaponSO);

        _animator.Play(AnimationNames.Shoot, 0, 0f); // Animation / Layer / Start Frame
    }
}
