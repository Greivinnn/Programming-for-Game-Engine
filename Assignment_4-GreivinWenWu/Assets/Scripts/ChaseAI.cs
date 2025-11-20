using UnityEngine;
using UnityEngine.AI;

public class ChaseAI : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;  // Drag the player here
    [SerializeField]
    private float updateInterval = 0.2f;  // How often to update path (seconds)

    private NavMeshAgent agent = null;
    private float updateTimer = 0.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target == null || agent == null)
        {
            return;
        }

        // Update path at intervals instead of every frame for performance
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0.0f)
        {
            agent.SetDestination(target.transform.position);
            updateTimer = updateInterval;
        }
    }
}
