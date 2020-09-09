using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
  
    
    public AudioSource audiosource;
    public enum stage {Start,Dharug,English,None};
    public stage level;
     public int Fileno = 0;
    public AudioClip[] listAudio;
    public string[] dharug;
    public string[] translations;
  
    public new Camera camera;
    public bool play,next;
    AudioClip audioclip;
    public GameObject clouds;
    public Text text;
    public string location = "DA";
	int storeFileno =0;
    
    PDPortSend pdsend;
    // Start is called before the first frame update
    void Start()
    {
        play = true;
        next = true;
        Fileno = 0;
        //clouds = GameObject.FindWithTag("clouds");
        try
        {
            level = (stage)System.Enum.Parse(typeof(stage), PlayerPrefs.GetString("StoredStage"));
        }
        catch 
        {
           // PlayerPrefs.SetString("StoredStage", stage.Start.ToString());
            level = stage.Start;
        }
        //not sure why need this
      

        if(level == stage.English) level = stage.Start;
        if (level == stage.Dharug) level = stage.English;
        if (level == stage.None) level = stage.Dharug;
        if (level == stage.Start) level = stage.None;
        //level = stage.None;
        PlayerPrefs.SetString("StoredStage", level.ToString());


        audiosource = transform.GetComponent<AudioSource>();
        Text [] texts = FindObjectsOfType<Text>();
        //  text= canvas.GetComponent<Text>();
       // Debug.Log(texts);
        text = texts[0];
       // pdsend=GetComponent<PDPortSend>();
    }

    // Update is called once per frame
    void Update()
    {
        if (location == "DA") next = true;
        else if (Input.GetKeyDown(KeyCode.C))
        {
            next = true;
            //revert to saved
           // level = (stage)System.Enum.Parse(typeof(stage), PlayerPrefs.GetString("StoredStage"));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
			Fileno=storeFileno-1;
            next = true;
            //update stage
			if (level == stage.English) level = stage.Start;
            if (level == stage.Dharug) level = stage.English;
            if (level == stage.None) level = stage.Dharug;
            if (level == stage.Start) level = stage.None;

        }
       

            StartCoroutine(RunAudio());
        
    }
  
    IEnumerator RunAudio()
    {
        
        while (Fileno < 12)
        {
            if (next& play & !audiosource.isPlaying)
            {
                
                //audioclip = Resources.Load < AudioClip>(url);
                audiosource.clip = listAudio[Fileno];
                //  Debug.Log(audiosource.clip.length);

                
               

                if (level == stage.Dharug) text.text = dharug[Fileno];
                else if (level == stage.English) text.text = translations[Fileno];
                else text.text = "";
                Fileno += 1;
				storeFileno=Fileno;

                audiosource.PlayDelayed(1f);
                
                //               pdsend.sendMessagePD("2 2");

                //           pdsend.sendMessagePD("2 1");
                // Debug.Log(level + " " + Fileno);
                yield return new WaitForSeconds(1f);


                next = false;
                //added one to fileno
                if (Fileno == 7)
                {

                    
                    clouds.SetActive(true);
                }
                if (Fileno == 11)
                {

                   
                    clouds.SetActive(false);
                }
                
            }
    
            yield return new WaitUntil(() => !audiosource.isPlaying);
            if (location!="DA")text.text = "Press C to continue or R to repeat phrase";


        }
    }
    }


