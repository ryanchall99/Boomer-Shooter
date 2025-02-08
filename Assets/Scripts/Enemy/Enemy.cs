using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform Player;

    NavMeshAgent _navMeshAgent;

    private void Awake() 
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();    
    }

    private void Update() 
    {
        _navMeshAgent.SetDestination(Player.position);    
    }
}
