using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;
    public int Damage;
    public int Range;
    public float FireRate;

    public GameObject HitVFXPrefab;

    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomAmount = 10f;
}
