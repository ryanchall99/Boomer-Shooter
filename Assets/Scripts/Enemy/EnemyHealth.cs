using UnityEngine;

public class EnemyHealth : BaseHealth
{
    [SerializeField] GameObject ExplosionVFX;

    public override void Death()
    {
        // Instantiate VFX Prefab
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
