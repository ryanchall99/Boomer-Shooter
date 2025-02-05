using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform Player;

    NavMeshAgent _NavMeshAgent;

    private void Awake() 
    {
        _NavMeshAgent = GetComponent<NavMeshAgent>();    
    }

    private void Update() 
    {
        _NavMeshAgent.SetDestination(Player.position);    
    }
}
