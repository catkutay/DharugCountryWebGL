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
            move -= 1f;
            
        }
        if (Input.GetKeyDown("d") || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            move += 1f;
           
        }
        if (Input.GetKeyDown("w") || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            
            
        }
        if (Input.GetKeyDown("s") || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            
           
        }
       
        
        transform.Rotate( Vector3.up, move * Time.deltaTime);
        
       // transform.Rotate(RotationY, 0, 0);
        //transform.Translate(new Vector3(move, 0, move));
	
    }
}