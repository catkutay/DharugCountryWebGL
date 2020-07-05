using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment2D : MonoBehaviour
{
    private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);
    bool Left;
    bool Right;
    bool orbit;
    bool Collider;
    public string ColliderTag = "Nothing";

    void Start()
    {
        orbit = true;
      
    }

    void Update()
    {

        if (Input.GetKeyDown("a") && orbit == true)
        {
            Left = true;
            Right = false;
        }

        if (Input.GetKeyDown("d") && orbit == true)
        {
            Right = true;
            Left = false;
        }

        if (Left == true && orbit == true)
        {
            transform.RotateAround(origin, Vector3.up, -30 * Time.deltaTime);

        }
        if (Right == true && orbit == true)
        {
            transform.RotateAround(origin, Vector3.up, 30 * Time.deltaTime);

        }

        if (transform.position.y >= 0.05f)
        {
            orbit = true;
        }

        if (orbit == false)
        {
            if (Input.GetKeyDown("space"))
            {
                //set game object up to position 0
                transform.position = new Vector3(transform.position.x, +2f, transform.position.z);
            }


        }

        if (Input.GetKeyDown("space") && Collider == true)
        {
            orbit = false;
            transform.position = new Vector3(transform.position.x, -0.1f, transform.position.z);
            //PlayAudio();

            /*
        if( game object tag1 == this)    
        {
        ColliderTag = "Audio1";
        orbit = fasle;
        transform.position = new Vector3(transform.position.x, -2, transform.position.z);
        }
         */

        }


    }
    void OnTriggerEnter(Collider collision)
    {
        Collider = true;
        Debug.Log("Collided");
       // return collider tag
    }
     void OnTriggerExit(Collider C)
     {
         Collider = false;
         Debug.Log("Exited");
         //retun collider tag
     }
    /*
     private void PlayAudio()
     {
         if( ColliderTag == "Audio1" )
         {
          //play Audio clip 1

         }
         if (ColliderTag == "Audio2" )
         {
          //play Audio clip 2 

         }



     }

     */
}

