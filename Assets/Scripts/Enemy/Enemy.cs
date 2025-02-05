using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;

    NavMeshAgent m_NavMeshAgent;

    private void Awake() 
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();    
    }

    private void Update() 
    {
        m_NavMeshAgent.SetDestination(player.position);    
    }
}
