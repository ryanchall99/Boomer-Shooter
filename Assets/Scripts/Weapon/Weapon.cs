using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] LayerMask InteractionLayers;

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;
        MuzzleFlash.Play();

        /* --- DEBUGGING (REMOVE LATER) --- */
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * weaponSO.Range, Color.red, 30f);
        /* ---------------------------------*/

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weaponSO.Range, InteractionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);

            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
