using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
        
    public GameObject GOup;

    
    public GameObject GOleft;

    
    public GameObject GOright;

   
    public GameObject GOdown;

    void Start()
    {
       
    }

       void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
         GOup.SetActive(true);
        }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
           
            GOup.SetActive(false);
            }

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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GOdown.SetActive(true);
        }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
            GOdown.SetActive(false);
            }


    }
}
