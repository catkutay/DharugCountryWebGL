using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateImage : MonoBehaviour
{
    public float speed;
    Vector3 rotationEuler=new Vector3 (0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // rotationEuler += new Vector3(1,0,1) * 3 * Time.deltaTime; //increment 3 degrees every second
       // transform.rotation = Quaternion.Euler(rotationEuler);
        transform.Rotate(0f,0f, speed/1000,Space.Self);
    }
}