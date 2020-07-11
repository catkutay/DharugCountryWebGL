using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoController : MonoBehaviour
{
    public GameObject[] videoplayers;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject videoplayer in videoplayers){
           // Debug.Log("name "+ videoplayer.name);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
