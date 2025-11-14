using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    enum BehaviourState
    {
        Wander,
        Seek
    }

    public class WanderData
    {
        public float minUpdateTime = 3.0f;
        public float maxUpdateTime = 5.0f;
        public float currentUpdateTime = 0.0f;
        public float moveRange = 5.0f;
        public Vector3 centerPoint = Vector3.zero;
        public Vector3 currentTarget = Vector3.zero;
    }

    public class SeekData
    {
        public Vector3 lastTargetPosition = Vector3.zero;
        public float cantFindTime = 5.0f;
        public float currentCantFindTime = 0.0f;
    }

    [SerializeField]
    private GameObject objective = null;
    [SerializeField]
    private float viewDistance = 10.0f;

    private NavMeshAgent agent = null;
    BehaviourState behaviourState = BehaviourState.Wander;
    private WanderData wanderData = new WanderData();
    private SeekData seekData = new SeekData();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    public void SetDestination(Vector3 destination)
    {
        if (agent != null)
        {
            agent.SetDestination(destination);
        }
    }

    private void Update()
    {
        switch (behaviourState)
        {
            case BehaviourState.Wander:
                DoWander();
                break;
            case BehaviourState.Seek:
                DoSeek();
                break;
                default:
                break;
        }
    }

    private void StartWander()
    {
        Vector2 radiusOffset = UnityEngine.Random.insideUnitCircle.normalized * wanderData.moveRange;
        wanderData.currentTarget.x = wanderData.centerPoint.x + radiusOffset.x;
        wanderData.currentTarget.z = wanderData.centerPoint.z + radiusOffset.y;
        wanderData.currentTarget.y = wanderData.centerPoint.y;
        wanderData.currentUpdateTime = UnityEngine.Random.Range(wanderData.minUpdateTime, wanderData.maxUpdateTime);
        SetDestination(wanderData.currentTarget);
        behaviourState = BehaviourState.Wander;
    }

    private void StartSeekState()
    {
        
        seekData.lastTargetPosition = objective.transform.position;
        SetDestination(seekData.lastTargetPosition);
        behaviourState = BehaviourState.Seek;
    }

    private bool CanSeeObjective()
    {
        if(objective != null)
        {
            Vector3 directionToObjective = objective.transform.position - transform.position;
            directionToObjective.Normalize();
            if (Physics.Raycast(transform.position, directionToObjective, out RaycastHit hitInfo, viewDistance))
            {
                if (hitInfo.collider != null && hitInfo.collider.gameObject == objective)
                {
                    Debug.DrawLine(transform.position, hitInfo.point, Color.green, 1.0f);
                    return true;
                }
                Debug.DrawLine(transform.position, hitInfo.point, Color.red, 1.0f);
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + directionToObjective * viewDistance, Color.red, 1.0f);
            }
        }
        return false;
    }

    private void DoWander()
    {
        wanderData.currentUpdateTime -= Time.deltaTime;
        if (wanderData.currentUpdateTime <= 0.0f || Vector3.Distance(transform.position, wanderData.currentTarget) <= 1.0f)
        {
            StartWander();
        }
        if(CanSeeObjective())
        {
            StartSeekState();
        }
    }

    private void DoSeek()
    {
        if (!CanSeeObjective())
        {
            seekData.currentCantFindTime -= Time.deltaTime;
            if (seekData.currentCantFindTime <= 0.0f)
            {
                DoWander();
            }
        }
        else
        {
            seekData.currentCantFindTime = seekData.cantFindTime;
            seekData.lastTargetPosition = objective.transform.position;
        }
        SetDestination(seekData.lastTargetPosition);        
    }
}
