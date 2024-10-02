using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PedestrianNavigation : MonoBehaviour
{
    public float movementSpeed = 1;
    public float rotationSpeed = 120;
    public float stopDistance = 2f;
    public Vector3 destination;
    public Animator animator;
    public bool reachedDestination;

    private Vector3 lastPosition;
    public Vector3 velocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position != destination) {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else {
                reachedDestination = true;
            }

            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);

            animator.SetFloat("Horizontal", rightDotProduct);
            animator.SetFloat("Forward", fwdDotProduct);
            //geia
            //se exw hakarei
            //sus
        }
        lastPosition = transform.position;
    }

    public void setDestination(Vector3 destination) {
        this.destination = destination;
        reachedDestination = false;
    }
}
