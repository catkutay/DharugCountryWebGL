using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    private float angleY;
    private float angleZ;
    private float Rotate;
    private float Forward;
    //private bool Right;
    private float Distance;
    
    private bool Back;
    public GameObject MidPoint;
    public Quaternion MidPointRotation;
    public Vector3 MidPointVector;
    public Transform MidPointTransform;

    public Quaternion lookAtRotation;

    private Rigidbody RB;


    void Start()
    {
        RB = GetComponent<Rigidbody>();
        angleY = 0;
        Forward = 70;
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(this.transform.position, MidPoint.transform.position);
        RB.velocity = transform.forward * Forward;
        if (Input.GetKeyDown("w") || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            //Rotate to the left
            Forward += 10;
           
        }
        if (Input.GetKeyUp("w") || (Input.GetKeyUp(KeyCode.UpArrow)))
        {
            Forward -= 10;
        }


        if (Input.GetKeyDown("a") || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            //Rotate to the left
           angleY -= .6f;
        }
            if (Input.GetKeyUp("a") || (Input.GetKeyUp(KeyCode.LeftArrow)))
            {
                //Rotate to the left
                angleY += .6f;
            }

        if (Input.GetKeyDown("d") || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            angleY += .6f;
            //Rotate to the right
        }
            if (Input.GetKeyUp("d") || (Input.GetKeyUp(KeyCode.RightArrow)))
            {
                angleY -= .6f;
                //Rotate to the right
            }


        if (Distance > 3000 )
           {
            lookAtRotation = Quaternion.LookRotation(MidPointTransform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation ,Time.deltaTime/2f);
       // rotates the player towards the MidPoint if they are too far
        }
       
        
        if (this.transform.rotation.eulerAngles.z > 30f && this.transform.rotation.eulerAngles.z < 5f )
        {
            angleZ -= .5f;
            //Debug.Log("30>" );
        }

        if (this.transform.rotation.eulerAngles.z < -30f && this.transform.rotation.eulerAngles.z > -5f )
        {
            angleZ += .5f;

        }

        //Debug.Log("Distance is : " + Distance);
        transform.Rotate(0, angleY, angleZ);
        //transform.rotatioan.eulerAngles.z = 0; 
    }

    }
