using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    float speed=0f;
	float move=0f;
   
    float RotationY;
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("a") || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            if (move > 0) move = 0;
            move += -5f;
            
        }
        if (Input.GetKeyDown("d") || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if (move < 0) move = 0;
            else move += 5f;
       
           
        }
       
        
        transform.Rotate( Vector3.up, move * Time.deltaTime);
       // move = 0;
       // transform.Rotate(RotationY, 0, 0);
        //transform.Translate(new Vector3(move, 0, move));
	
    }
}