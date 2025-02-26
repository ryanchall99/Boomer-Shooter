using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float Radius = 1.5f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public void Explode()
    {
        // DO DAMAGE TO PLAYER
    } 
}
