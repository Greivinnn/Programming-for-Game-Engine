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
        public int currentWaypointIndex = 0;
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
    [SerializeField]
    private WaypointManager assignedPath = null;
    [SerializeField]
    private bool startAtClosestWaypoint = true;

    private NavMeshAgent agent = null;
    BehaviorState behaviorState = BehaviorState.Wander;
    private WanderData wanderData = new WanderData();
    private SeekData seekData = new SeekData();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        wanderData.centerPoint = transform.position;

        // Start at the closest waypoint if enabled
        if (startAtClosestWaypoint && assignedPath != null)
        {
            Waypoint closest = assignedPath.GetClosestWaypoint(transform.position);
            if (closest != null)
            {
                wanderData.currentWaypointIndex = assignedPath.GetWaypointIndex(closest);
            }
        }

        StartWander();
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
        if (assignedPath == null)
        {
            Debug.LogWarning("No waypoint path assigned to AI Agent: " + gameObject.name);
            return;
        }

        Waypoint waypoint = assignedPath.GetWaypoint(wanderData.currentWaypointIndex);

        if (waypoint != null)
        {
            wanderData.currentTarget = waypoint.transform.position;
            wanderData.currentUpdateTime = UnityEngine.Random.Range(wanderData.minUpdateTime, wanderData.maxUpdateTime);
            SetDestination(wanderData.currentTarget);
            behaviorState = BehaviorState.Wander;

            // Move to next waypoint in sequence
            wanderData.currentWaypointIndex++;

            // Loop back to start if we've reached the end
            if (wanderData.currentWaypointIndex >= assignedPath.GetWaypointCount())
            {
                wanderData.currentWaypointIndex = 0;
            }
        }
    }

    private void StartSeek()
    {
        seekData.lastTargetPosition = objective.transform.position;
        seekData.currentCantFindTime = seekData.cantFindTime;
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
                StartWander();
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
