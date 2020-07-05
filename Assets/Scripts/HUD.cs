using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(nextSceneIndex); 
        }
    }
}
