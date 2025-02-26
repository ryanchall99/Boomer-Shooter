using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private PlayerController _player;
    private NavMeshAgent _navMeshAgent;

    private void Awake() 
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();    
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
}
