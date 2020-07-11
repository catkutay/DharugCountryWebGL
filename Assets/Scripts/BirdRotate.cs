using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRotate : MonoBehaviour
{
    private bool Left;
    private bool Right;
    private float RotationZ;
   
    // Start is called before the first frame update
    void Start()
    {
        Left = false;
        Right = false;


    }

    // Update is called once per frame
    void Update()
    {
 	

        if (Input.GetKeyDown("a") || (Input.GetKeyDown(KeyCode.LeftArrow)) )
        {
            Left = true;
        }
        if (Input.GetKeyUp("a") || (Input.GetKeyUp(KeyCode.LeftArrow)))
        {
            Left = false;
            
        }
        if (Input.GetKeyDown("d") || (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            Right = true;
        }
        if (Input.GetKeyUp("d") || (Input.GetKeyUp(KeyCode.RightArrow)))
        {
            Right = false;
        }

        if( Left == true)
        {
            if (RotationZ < 30)
            {
                RotationZ += 0.5f;
                        
            }

        }
        if (Right == true)
        {
            if (RotationZ > -30)
            {
                RotationZ -= 0.5f;

            }

        }



        if (RotationZ > 0 && Left == false)
            {
                RotationZ -= 1.3f;
            }
            if (RotationZ < 0 && Right == false)
            {
                RotationZ += 1.3f;
            }

      
       transform.localEulerAngles = new Vector3(0, 0,RotationZ);
        

       
    }

   

}
