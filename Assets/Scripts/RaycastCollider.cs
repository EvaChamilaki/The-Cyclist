using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollider : MonoBehaviour
{
    public float range = 60;
    private float deceleration = 10f;
    private float acceleration = 15f;

    void Update()
    {
        Vector3 direction = Vector3.forward;
        Ray ray = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range), Color.blue);

        if(Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (gameObject.tag == "YellowBike" && (hit.collider.tag == "Pedestrian" || hit.collider.tag == "RedBike"))
            {
                float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
                float deceleratedSpeed = Mathf.Max(0,currentSpeed - deceleration*Time.deltaTime);
                this.GetComponent<BicycleNavigator>().movementSpeed = deceleratedSpeed;
                

            }
            else if (gameObject.tag == "RedBike" && (hit.collider.tag == "Pedestrian" || hit.collider.tag == "YellowBike"))
            {
                float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
                float deceleratedSpeed = Mathf.Max(0,currentSpeed - deceleration*Time.deltaTime);
                this.GetComponent<BicycleNavigator>().movementSpeed = deceleratedSpeed;
                

            }
            //for the second scene
            if (gameObject.tag == "YellowBike1" && (hit.collider.tag == "Pedestrian" || hit.collider.tag == "YellowBike2" || hit.collider.tag == "RedBike1" || hit.collider.tag == "RedBike2"))
            {
                float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
                float deceleratedSpeed = Mathf.Max(0, currentSpeed - deceleration * Time.deltaTime);
                this.GetComponent<BicycleNavigator>().movementSpeed = deceleratedSpeed;
                
            }
            else if (gameObject.tag == "YellowBike2" && (hit.collider.tag == "Pedestrian" || hit.collider.tag == "YellowBike1" || hit.collider.tag == "RedBike1" || hit.collider.tag == "RedBike2"))
            {
                float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
                float deceleratedSpeed = Mathf.Max(0, currentSpeed - deceleration * Time.deltaTime);
                this.GetComponent<BicycleNavigator>().movementSpeed = deceleratedSpeed;
                
            }
            else if (gameObject.tag == "RedBike1" && (hit.collider.tag == "Pedestrian" || hit.collider.tag == "YellowBike2" || hit.collider.tag == "YellowBike1" || hit.collider.tag == "RedBike2"))
            {
                float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
                float deceleratedSpeed = Mathf.Max(0, currentSpeed - deceleration * Time.deltaTime);
                this.GetComponent<BicycleNavigator>().movementSpeed = deceleratedSpeed;
                
            }
            else if (gameObject.tag == "RedBike2" && (hit.collider.tag == "Pedestrian" || hit.collider.tag == "YellowBike2" || hit.collider.tag == "YellowBike1" || hit.collider.tag == "RedBike1"))
            {
                float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
                float deceleratedSpeed = Mathf.Max(0, currentSpeed - deceleration * Time.deltaTime);
                this.GetComponent<BicycleNavigator>().movementSpeed = deceleratedSpeed;
                
            }

        }
        else
        {
            float currentSpeed = this.GetComponent<BicycleNavigator>().movementSpeed;
            float acceleratedSpeed = Mathf.Min(20, currentSpeed + acceleration * Time.deltaTime);
            this.GetComponent<BicycleNavigator>().movementSpeed = acceleratedSpeed;
           
        }
    }
}
