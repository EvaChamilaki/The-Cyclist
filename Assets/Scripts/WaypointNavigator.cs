using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    PedestrianNavigation controller;
    public Waypoint currentWaypoint;
    public int direction;

    private void Awake()
    {
        controller = GetComponent<PedestrianNavigation>();
        controller.setDestination(currentWaypoint.GetPosition());
        direction = Mathf.RoundToInt(UnityEngine.Random.Range(0, 1));
    }

    void Update()
    {
        if (controller.reachedDestination)
        {
            bool shouldBranch = false;
            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = (Random.Range(0f, 1f) <= currentWaypoint.branchRatio) ? true : false;
            }
            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if(currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.prevWaypoint;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if(currentWaypoint.prevWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.prevWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                }
            }

           controller.setDestination(currentWaypoint.GetPosition());
        }
    }
}
