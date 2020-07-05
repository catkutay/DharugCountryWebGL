using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    PlayAudio remote;
    AudioSource audioSource, remoteSource;
    
    public PlayAudio.stage level ;
    PlayAudio playaudio;
    public string dharug;
    public string translations;
   
    int number;

    public Text text;
    private void OnTriggerEnter(Collider Coll)
    {
       
         remote = Coll.GetComponent<PlayAudio>();
        number = remote.Fileno;
         remoteSource = Coll.GetComponent<AudioSource>();
       
        // remoteSource.Pause();
    
         playaudio = remote.GetComponent<PlayAudio>();
        level = playaudio.level;
        playaudio.play = false;
        //local source
        audioSource = gameObject.GetComponent<AudioSource>();

        text = playaudio.text;
       
        //pdsend=GetComponent<PDPortSend>();

        StartCoroutine(stashAudio());
        
    }
    IEnumerator stashAudio() {
        //stop remote
        //while (remoteSource.isPlaying)  yield return new WaitForSeconds(1f);
       
        text.text = "";
        //start local
        audioSource.Play();
        if (level == PlayAudio.stage.Dharug) text.text = dharug;
        else if (level == PlayAudio.stage.English) text.text = translations;
        else text.text = "";
       
        yield return new WaitUntil(() => !audioSource.isPlaying);
        if (!audioSource.isPlaying)
        {
            //
           // 
           // remoteSource.UnPause();
            
           // remote.Fileno = number;
            //remoteSource.Play();
            playaudio.play = true;
        }
            if (gameObject.tag == "POI1")
            {
                //
                Debug.Log("POI1");
            }
            if (gameObject.tag == "POI2")
            {
                //whale animates?
                Debug.Log("POI2");
            }
            if (gameObject.tag == "POI3")
            {
            //Audio 3
            PlayerPrefs.SetString("StoredStage", playaudio.level.ToString());


            SceneManager.LoadScene("2DScene");
                Debug.Log("POI3");
            }

        



    }

}
