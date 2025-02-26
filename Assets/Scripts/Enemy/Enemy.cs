using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private PlayerController _player;
    private NavMeshAgent _navMeshAgent;
    private EnemyHealth _enemyHealth;

    const string PLAYER_STRING = "Player";

    private void Awake() 
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        _player = FindFirstObjectByType<PlayerController>();
    }

    private void Update() 
    {
        if (!_player)
        {
            Debug.LogError("No Player Character Found!");
            return;
        }
        
        _navMeshAgent.SetDestination(_player.transform.position);    
    }

    void OnTriggerEnter(Collider other)
    {
        // Checking other tag.
        if (other.CompareTag(PLAYER_STRING))
        {
            _enemyHealth.Death();
        }
    }
}
