using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoSorter : MonoBehaviour
{
    public enum stage { Start, Dharug, English, None };
    public Text text;
    public stage level = stage.Start;
    public bool change, play, next;
    public int Fileno = 0, storedFileno=0;
    public VideoSorter figure;
    public GameObject figures;
    //public GameObject complete;
    public AudioClip[] listAudio;
    public string[] dharug;
    public string[] translations;
    public VideoSorter Follow1, Follow2;
    public VideoClip[] listVideo;
    public string[] listOfVideos;
    public MeshRenderer parentMesh;
    public string location="DA";
    string url;
    string folder = "Videos";

    VideoPlayer videoplayer;

    //fix when get magpie

    AudioSource audiosource;
    AudioClip audioclip;
    //PDPortSend pdsend;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            level = (stage)System.Enum.Parse(typeof(stage), PlayerPrefs.GetString("StoredStage"));
        }
        catch
        {
            PlayerPrefs.SetString("StoredStage", stage.Start.ToString());
            level = stage.Start;
        }
        if (level == stage.English) level = stage.Start;
        if (level == stage.Dharug) level = stage.English;
        if (level == stage.None) level = stage.Dharug;
        if (level == stage.Start) level = stage.None;
       // level = stage.Start;
        PlayerPrefs.SetString("StoredStage", level.ToString());

        videoplayer = transform.GetComponent<VideoPlayer>();

        //   Text[] texts = FindObjectsOfType<Text>();
        //text= GetComponent<Text>();
        //  text = texts[0];
        audiosource = transform.GetComponent<AudioSource>();
       // Debug.Log(texts);

        //   pdsend = GetComponent<PD2dPortSend>();

#if UNITY_EDITOR_OSX

        videoplayer.clip = listVideo[0];


#elif UNITY_WEBGL

if (folder=="")
    url = System.IO.Path.Combine(Application.streamingAssetsPath,listOfVideos[0]);
else
       url = System.IO.Path.Combine(Application.streamingAssetsPath, folder, listOfVideos[0]);

      videoplayer.url = url;
      

#else
        videoplayer.clip = listVideo[Fileno];
        
#endif
    videoplayer.Prepare();
        play = true;
        next = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (location=="DA")next = true;
        else if (Input.GetKeyDown(KeyCode.C))
        {
            next = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Fileno =storedFileno-1;
            next = true;
        }
        StartCoroutine(RunVideo());
        //InvokeRepeating("checkOver", .1f, .1f);
    }

    IEnumerator RunVideo()
    {
        
        //activate next speaker
            storedFileno=Fileno;
            if (name == "Magpie" & Fileno == 4)
            {
                // figures = GameObject.FindGameObjectWithTag("Figures");
                if (figures)figures.SetActive(true);
                if (figure)figure.change=true;
            }
            if (name == "Kangaroo" & Fileno == 4)
            {
                // figures = GameObject.FindGameObjectWithTag("Figures");
                 if (figures)figures.SetActive(true);
                if (figure) figure.change=true;
               
            }
           if (name == "Kangaroo" & Fileno > 4)
           {
               if (Camera.main.transform.eulerAngles.y < 35 | Camera.main.transform.eulerAngles.y > 298)
                   Camera.main.transform.Rotate(Vector3.up, -5 * Time.deltaTime);
           }
        //

        //stop looping for talking videos
        if (Fileno == listVideo.Length - 1){
                    //final script - generalise FIXME
                    //text.text = translations[Fileno];

                    videoplayer.isLooping = true;
                   // level = stage.English;
        } else videoplayer.isLooping = false;
       
        //move to next video segment
        if (Fileno < listVideo.Length)
        {
            if (next & play & change & !videoplayer.isPlaying)
            {
                change = false;

                //pdsend.sendMessagePD("3 2");
                audiosource.clip = listAudio[Fileno];
                
#if UNITY_EDITOR_OSX

                videoplayer.clip = listVideo[Fileno];


#elif UNITY_WEBGL
if (folder=="")
url = System.IO.Path.Combine(Application.streamingAssetsPath,  listOfVideos[0]);
else 
                url = System.IO.Path.Combine(Application.streamingAssetsPath, folder, listOfVideos[Fileno]);

                videoplayer.url = url;

#else
                videoplayer.clip = listVideo[Fileno];   
       
#endif
               // videoplayer.Prepare();
                //yield return new WaitForSeconds(2f);
               
                videoplayer.Play();
                // Missing sound with video
				if ((name=="Man Standing" & Fileno >2)|Fileno==0) audiosource.Play();
                
                if (level == stage.Dharug) text.text = dharug[Fileno];
                else if (level == stage.English) text.text = translations[Fileno];
                else text.text = "";

                parentMesh = this.GetComponentInParent(typeof(MeshRenderer)) as MeshRenderer;
               
                //wait for vidoe to load before enabling mesh
                
                if (name=="Magpie") yield return new WaitForSeconds(2f);
                else yield return new WaitForSeconds(5f);
               /*  else if (name == "Woman")
                    yield return new WaitForSeconds(4f);
                else yield return new WaitForSeconds(2f);*/ 
                
                if (parentMesh & videoplayer.isPlaying) parentMesh.enabled = true;

               
                //add end loop audio
                //put on loop
                
                if (name == "Man Standing" & (Fileno == 1 | Fileno == 2))
                {
                    play = false;
                    Follow1.play = true;
                    //pass speech
                    Follow1.Fileno = Fileno;
                }
                if (name == "Man Sitting" & (Fileno == 1 | Fileno == 2))
                {

                    if (Fileno==1)play = false;//else loop next round
                    
                    Follow1.play = true;
                    //pass speech
                    Follow1.Fileno = Fileno;
                    
                }
                if (name == "Man Standing" & (Fileno == 3 | Fileno == 4))
                {
                    if (Fileno==3)play = false; //else loop next round
                    Follow2.play = true;
                    //pass speech
                    Follow2.Fileno = Fileno -2;
                    //allow woman to change
                    figure.next=true;
                    
                }
                if (name == "Woman" & (Fileno == 1 | Fileno == 2))
                {
                    if (Fileno==1)play = false; //else loop next round
                    Follow1.play = true;
                    //pass speech
                    Follow1.Fileno =Fileno+2;
                }
                //wait after first language spoken unless about to enter last loop
                if (Fileno > 0 & Fileno<listVideo.Length-2)
                {
                    next = false;
                   
                }
        //turn off previous
        
                
                    Fileno += 1;
                    storedFileno=Fileno;
                    //not working
                //yield return new WaitForSeconds(1f);
                videoplayer.loopPointReached += CheckOver;
                
                if (name == "Man Standing" & Fileno ==listVideo.Length)
                {
                    PlayerPrefs.SetString("StoredStage", level.ToString());
                    SceneManager.LoadScene("(Scene2)2DWorldView");
                }
                
                //pdsend.sendMessagePD("3 1");
                yield return new WaitUntil(() => !audiosource.isPlaying);
                yield return new WaitUntil(() => !videoplayer.isPlaying);
                
              if (location!="DA")  text.text = "Press C to continue or R to repeat phrase";
                //  yield return new WaitForSeconds((float)videoplayer.length);


            }

        }
        else this.gameObject.SetActive(false);


    }
    //seems to work
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        change = true;
        //Debug.Log("Changeing");

    }


}

