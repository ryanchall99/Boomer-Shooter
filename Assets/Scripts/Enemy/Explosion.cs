using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float Radius = 1.5f;
    [SerializeField] int Damage = 3;

    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if (!playerHealth) continue; // Move onto next iteration in loop early

            playerHealth.TakeDamage(Damage);

            break; // Break out of loop (Player Health Found)
        }
    } 
}
