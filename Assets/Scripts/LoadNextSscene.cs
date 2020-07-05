using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSscene : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Next;



    private void OnTriggerEnter(Collider Coll)
    {
        Next = true;
    }
    private void OnTriggerExit(Collider Coll)
    {
        Next = false;

    }
    void Update()
    {


        if (Input.GetKeyDown("n") && Next == true)
        {

        
            NextScene();
        }
    }


    public void NextScene()
    {
        SceneManager.LoadScene("(Scene2)2DWorldView");

    }




}
