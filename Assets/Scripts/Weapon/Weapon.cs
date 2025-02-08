using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem MuzzleFlash;

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;

        MuzzleFlash.Play();
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * weaponSO.Range, Color.red, 30f);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponSO.Range))
        {
            if (hit.collider.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(weaponSO.Damage);
            }
        }
    }
}
