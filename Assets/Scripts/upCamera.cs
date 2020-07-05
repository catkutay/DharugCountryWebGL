using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upCamera : MonoBehaviour
{
    // Start is called before the first frame update
  
    bool Up, Down;
    float RotationY;
    void Start()
    {
        Up = false;
        Down = false;
        RotationY = 0;


    }

    // Update is called once per frame
    void Update()
    {

      
        if (Input.GetKeyDown("w") )
        {
            Up = true;
            Down = false;
        }
        if (Input.GetKeyDown("s") )
        {
            Up = false;
            Down = true;

        }
        if (Input.GetKeyUp("w") ||Input.GetKeyUp("s") )
        {
            Up = false;
            Down = false;
        }
        if (Up == true)
        {

            RotationY = -0.3f;


        }
   


        if (Down == true)
        {
           
            
                RotationY = 0.3f;
            

        }
        if (!Up && !Down) RotationY = 0f;
      
        transform.Rotate(RotationY, 0, 0);
       
    }
}