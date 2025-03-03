using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float SpawnDelay;
    [SerializeField] Transform SpawnPoint;

    PlayerController player;

    void Start() 
    {
        player = FindFirstObjectByType<PlayerController>();

        StartCoroutine(SpawnEnemy());    
    }

    IEnumerator SpawnEnemy()
    {
        // While player is still alive...
        while(player)
        {
            // Instantiate Enemy At Spawn Point Every Spawn Delay In Seconds
            Instantiate(EnemyPrefab, SpawnPoint.position, transform.rotation);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

}
