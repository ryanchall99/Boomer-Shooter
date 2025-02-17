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
}
