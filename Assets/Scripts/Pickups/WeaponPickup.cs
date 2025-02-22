using UnityEngine;

public class WeaponPickup : BasePickup
{
    [SerializeField] WeaponSO weaponSO;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapons(weaponSO);    
    }
}
