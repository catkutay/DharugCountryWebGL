using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator2 : MonoBehaviour
{
        
    //public GameObject GOup;

    
    public GameObject GOleft;

    
    public GameObject GOright;

   
   // public GameObject GOdown;

    void Start()
    {
       
    }

       void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GOleft.SetActive(true);
        }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
            GOleft.SetActive(false);
            }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GOright.SetActive(true);
        }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
            GOright.SetActive(false);

            }

       


    }
}
