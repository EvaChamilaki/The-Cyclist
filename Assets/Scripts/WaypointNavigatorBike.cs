using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigatorBike : MonoBehaviour
{
    BicycleNavigator controller;
    public Waypoint currentWaypoint;
    int direction;

    private void Awake()
    {
        controller = GetComponent<BicycleNavigator>();
        controller.setDestination(currentWaypoint.GetPosition());
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
                    if (currentWaypoint.nextWaypoint != null)
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
                    if (currentWaypoint.prevWaypoint != null)
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
