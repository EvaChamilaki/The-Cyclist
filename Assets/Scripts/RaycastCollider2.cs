using System.Collections;
using UnityEngine;

public class RaycastCollider2 : MonoBehaviour //for the pedestrian-bike collision avoidance
{
    public float range = 20;
    public Animator animator;
    private float timer = 0f;
    public float stopTime = 2.0f;
    public bool isColliding = false;
    private bool isScriptEnabled = true;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 direction = Vector3.forward;
        Ray ray = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range), Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (gameObject.tag == "Pedestrian" && (hit.collider.tag == "YellowBike1" || hit.collider.tag == "YellowBike2" || hit.collider.tag == "RedBike1" || hit.collider.tag == "RedBike2"))
            {
                // If not already colliding, start the timer and stop the pedestrian.
                if (!isColliding)
                {
                    isColliding = true;
                    timer = 0f;
                    StopPedestrian();
                }
                else
                {
                    timer += Time.deltaTime;

                    // If the collision detection lasts longer than 3 seconds, change direction of the pedestrian so we can avoid traffic
                    if (timer >= stopTime)
                    {
                        //isScriptEnabled = true;
                        StartCoroutine(DisableScriptForSeconds(3f));
                    }
                }
            }
        }
        else
        {
            // If not colliding, resume pedestrian movement.
            animator.SetBool("TestBool", false);
            GetComponent<PedestrianNavigation>().enabled = true;
            isColliding = false;
            isScriptEnabled = true;
        }
    }

    void StopPedestrian()
    {
        GetComponent<PedestrianNavigation>().enabled = false; 
        animator.SetBool("TestBool", true); //pause the walking animation of the pedestrian and transition to "idle".
    }



 IEnumerator DisableScriptForSeconds(float seconds)
    {
        
        
        this.enabled = false;
        GetComponent<PedestrianNavigation>().enabled = true; 
        animator.SetBool("TestBool", false);
        //isScriptEnabled = false;

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Enable the script again
        
        this.enabled = true;
        //isScriptEnabled = true;
    }

}



