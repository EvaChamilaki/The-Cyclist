using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour //for the bicycle spawning
{
    public GameObject bicycle;
    private Waypoint initWaypoint;
    public int bicycleToSpawn;
    public List<Transform> specificWaypoints;

    void Start()
    {
        initWaypoint = bicycle.GetComponent<WaypointNavigatorBike>().currentWaypoint;
        bicycle.GetComponent<BicycleNavigator>().reachedDestination = false;
        
    }

    void Update()
    {
        var endOfPath = bicycle.GetComponent<WaypointNavigatorBike>().currentWaypoint;
        if (endOfPath != null && endOfPath.nextWaypoint == null) {
            bicycle.transform.position = this.transform.position;
            bicycle.GetComponent<WaypointNavigatorBike>().currentWaypoint = initWaypoint;
            bicycle.GetComponent<BicycleNavigator>().reachedDestination = true;
        }
    }
   
}


