using UnityEngine;

public class Weapon : MonoBehaviour
{
    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * weaponSO.Range, Color.red, 30f);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponSO.Range))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
