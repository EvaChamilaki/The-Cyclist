using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleNavigator : MonoBehaviour //sets the way the bikes will move
{
    public float movementSpeed;
    public float rotationSpeed;
    public float stopDistance = 2f;
    public Vector3 destination;
    public bool reachedDestination;

    public float destinationReachedDelay = 0.1f;
    private Vector3 lastPosition;
    Vector3 velocity;

    void Update()
    {
        if(!reachedDestination)
        {
            MoveTowardsDestination();
        }
    }

    void MoveTowardsDestination()
    {
        Vector3 destinationDirection = destination - transform.position;
        destinationDirection.y = 0;
        float destinationDistance = destinationDirection.magnitude;

        if (destinationDistance >= stopDistance)
        {
            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if(destinationDistance < stopDistance)
        {
            reachedDestination = true;
            StartCoroutine(DelayedReset());
        }
        velocity = (transform.position - lastPosition)/ Time.deltaTime;
        velocity.y = 0;
        lastPosition = transform.position;
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(destinationReachedDelay);
        setDestination(destination);
    }
    public void setDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
