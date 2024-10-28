using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyFoV : MonoBehaviour
{
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;
    [SerializeField] private Vector3 viewDirection; // Current direction navMeshAgent.destination - transform.position
    [SerializeField] private GameObject player;
    private NavMeshAgent NavMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayerTarget();
    }

    private void FindPlayerTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(viewDirection, directionToPlayer) > fov / 2)
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionToPlayer, viewDistance);
                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.CompareTag("Player"))
                    {
                        //Player is being seen
                        Debug.Log("This is the player");
                    }
                    else if (raycastHit.collider.CompareTag("Enemy"))
                    {
                        Debug.Log("This is the enemy!");
                    } else {
                        //It hit something else
                        Debug.Log("Object not player lmao");
                    }
                }
            }
        }
    }

    private void GetDirection()
    {
        if (NavMeshAgent.destination != Vector3.zero)
        {
            viewDirection = NavMeshAgent.destination - transform.position;
        }
    }
}
