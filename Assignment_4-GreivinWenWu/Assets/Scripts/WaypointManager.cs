using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    [SerializeField]
    private List<Waypoint> waypoints = new List<Waypoint>();

    // removed singleton pattern to be able to have multiple waypoint managers in the scene

    public Waypoint GetWaypoint(int index)
    {
        if(index < waypoints.Count)
        {
            return waypoints[index];
        }
        return null;
    }

    public int GetWaypointCount()
    {
        return waypoints.Count;
    }

    public int GetWaypointIndex(Waypoint waypoint)
    {
        return waypoints.IndexOf(waypoint);
    }

    public Waypoint GetRandomWaypoint()
    {
        if(waypoints.Count == 0)
        {
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, waypoints.Count);
        return waypoints[randomIndex];
    }

    public Waypoint GetClosestWaypoint(Vector3 position)
    {
        if(waypoints.Count == 0)
        {
            return null;
        }

        Waypoint closestWaypoint = null;
        float closestDistanceSqr = float.MaxValue;  
        foreach(Waypoint waypoint in waypoints)
        {
            float distanceSqr = Vector3.SqrMagnitude(waypoint.transform.position - position);
            if(distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }
}
