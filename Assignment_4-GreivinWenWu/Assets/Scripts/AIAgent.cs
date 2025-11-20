using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    enum BehaviorState
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
    private GameObject objective = null;        // this can be the player
    [SerializeField]
    private float viewDistance = 10.0f;
    private NavMeshAgent agent = null;
    BehaviorState behaviorState = BehaviorState.Wander;
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
        switch(behaviorState)
        {
            case BehaviorState.Wander:
                DoWander();
                break;
            case BehaviorState.Seek:
                DoSeek();
                break;
            default:
                break;
        }
    }

    private void StartWander()
    {
        //Vector2 radiusOffset = UnityEngine.Random.insideUnitCircle.normalized * wanderData.moveRange;
        //wanderData.currentTarget.x = wanderData.centerPoint.x + radiusOffset.x;
        //wanderData.currentTarget.z = wanderData.centerPoint.z + radiusOffset.y;
        //wanderData.currentTarget.y = wanderData.centerPoint.y;
        Waypoint WAYPOINT = WaypointManager.Instance.GetRandomWaypoint();
        WanderData currentWanderData = new WanderData();
        wanderData.currentUpdateTime = UnityEngine.Random.Range(wanderData.minUpdateTime, wanderData.maxUpdateTime);
        SetDestination(wanderData.currentTarget);
        behaviorState = BehaviorState.Wander;
    }

    private void StartSeek()
    {
        seekData.lastTargetPosition = objective.transform.position;
        SetDestination(seekData.lastTargetPosition);
        behaviorState = BehaviorState.Seek;
    }

    private bool CanSeeObjective()
    {
        if (objective != null)
        {
            Vector3 direction = objective.transform.position - transform.position;
            direction.Normalize();
            RaycastHit hitInfo;
            if(Physics.Raycast(transform.position, direction, out hitInfo))
            {
                if (hitInfo.collider != null)
                {
                    if(hitInfo.collider.gameObject == objective)
                    {
                        return true;
                    }
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direction * viewDistance, Color.red, 1.0f);
            }
        }

            return false;
    }

    private void DoWander()
    {
        wanderData.currentUpdateTime -= Time.deltaTime;
        if(wanderData.currentUpdateTime <= 0.0f || Vector3.Distance(transform.position, wanderData.currentTarget) <= 1.0f)
        {
            StartWander();
        }
        if (CanSeeObjective())
        {
            StartSeek();
        }
    }

    private void DoSeek()
    {
        if(!CanSeeObjective())
        {
            seekData.currentCantFindTime -= Time.deltaTime;
            if(seekData.currentCantFindTime <= 0.0f)
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
