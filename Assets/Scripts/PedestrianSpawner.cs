using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public int pedestriansToSpawn; 
    public List<Transform> specificWaypoints;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < pedestriansToSpawn) {
            GameObject obj = Instantiate(pedestrianPrefab);
            Transform waypoint = specificWaypoints[Random.Range(0, specificWaypoints.Count)]; //spawns pedestrians randomly to one of the selected waypoints
            
            obj.GetComponent<PedestrianNavigation>().reachedDestination = true;
            obj.GetComponent<WaypointNavigator>().currentWaypoint = waypoint.GetComponent<Waypoint>();
            obj.transform.position = waypoint.position;

            yield return new WaitForSecondsRealtime(3); //wait for three sec. before spawining another pedestrian
            count++;
        }
    }
}
