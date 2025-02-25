using UnityEngine;

public class AmmoPickup : BasePickup
{
    [SerializeField] private int AmmoAmount;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.AdjustAmmo(AmmoAmount);
    }
}
