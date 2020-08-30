using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class VideoSorter : MonoBehaviour
{
    public enum stage { Start, Dharug, English, None };
    public Text text;
    public stage level = stage.Start;
    public bool change, play, next;
    public int Fileno = 0;
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
      videoplayer.Prepare();

#else
        videoplayer.clip = listVideo[Fileno];
        videoplayer.Prepare();
#endif

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
            Fileno -= 1;
            next = true;
        }
        StartCoroutine(RunVideo());
        //InvokeRepeating("checkOver", .1f, .1f);
    }

    IEnumerator RunVideo()
    {
        //  Debug.Log(name+ play+ change);
        //Debug.Log(Camera.main.transform.eulerAngles);
        //activate next speaker
       
        if (change & name == "Magpie" & Fileno == 4)
        {

            // figures = GameObject.FindGameObjectWithTag("Figures");
            figures.SetActive(true);
        }
        if (change & name == "Kangaroo" & Fileno == 4)
        {

            // figures = GameObject.FindGameObjectWithTag("Figures");
            figures.SetActive(true);
        }
        if (name == "Man Sitting" | name=="Man Standing")
        {
            yield return new WaitForSeconds(1f);
            figures.SetActive(true);

        }

        //ebug.Log(Camera.main.transform.eulerAngles);
        // Move to next speaker
        if (name == "Magpie" & Fileno >= 0 & Fileno < 3)
        {

            if (Camera.main.transform.eulerAngles.y < 0 | Camera.main.transform.eulerAngles.y > 290)

                Camera.main.transform.Rotate(Vector3.up, 5 * Time.deltaTime);

        }
        if (name == "Magpie" & Fileno >= 3 & Fileno < listVideo.Length - 1)
        {

            if (Camera.main.transform.eulerAngles.y < 30 | Camera.main.transform.eulerAngles.y > 290)

                Camera.main.transform.Rotate(Vector3.up, 5 * Time.deltaTime);

        }
        if (name == "Kangaroo" & Fileno > 4)
        {


            if (Camera.main.transform.eulerAngles.y < 35 | Camera.main.transform.eulerAngles.y > 298)

                Camera.main.transform.Rotate(Vector3.up, -5 * Time.deltaTime);


        }
        //stop looping for talking videos
        if (0 < Fileno | Fileno < listVideo.Length - 1) videoplayer.isLooping = false;
        //else videoplayer.isLooping = true;
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

                //play background sounds;
                if (Fileno==0) audiosource.Play();
                if (Fileno == listVideo.Length - 1)
                {
                    //final script - generalise FIXME
                    text.text = translations[Fileno];

                    videoplayer.isLooping = true;
<<<<<<< HEAD
                    level = stage.English;
=======
                   // level = stage.English;
>>>>>>> 60f8430dcd9ac87f868378308a42102f1a76f35f
                }
                    videoplayer.Play();
                parentMesh = this.GetComponentInParent(typeof(MeshRenderer)) as MeshRenderer;
                
               
                //wait for vidoe to load before enabling mesh
                if (name =="Magpie" | name =="Kangaroo")
                    yield return new WaitForSeconds(1.5f);
                else if (name == "Woman")
                    yield return new WaitForSeconds(4f);
                else yield return new WaitForSeconds(2f);

                if (parentMesh & videoplayer.isPlaying) parentMesh.enabled = true;

                if (level == stage.Dharug) text.text = dharug[Fileno];
                else if (level == stage.English) text.text = translations[Fileno];
                else text.text = "";
                //add end loop audio
                //put on loop
<<<<<<< HEAD
                if ( Fileno == listVideo.Length - 1)//name == "Man Standing" | name == "Woman" |
=======
                if (name == "Man Standing" | name == "Woman" | Fileno == listVideo.Length - 1)
>>>>>>> 60f8430dcd9ac87f868378308a42102f1a76f35f
                {

                    audiosource.Play();
                    
<<<<<<< HEAD
                   // yield return new WaitUntil(() => !audiosource.isPlaying);
=======
                    yield return new WaitUntil(() => !audiosource.isPlaying);
>>>>>>> 60f8430dcd9ac87f868378308a42102f1a76f35f
                }

                //wait until pause then wake next figure
                
                yield return new WaitUntil(() => !videoplayer.isPlaying);
                if (location != "DA") text.text = "Press C to continue or R to repeat phrase";

                if (name == "Man Standing" & (Fileno == 1 | Fileno == 2))
                {
                    play = false;
                    Follow1.play = true;
                   // Follow1.Fileno += 1;
                }
                if (name == "Man Sitting" & (Fileno == 1 | Fileno == 2))
                {

                    play = false;
                    Follow1.play = true;
                   // Follow1.Fileno += 1;
                }
                if (name == "Man Standing" & (Fileno == 3 | Fileno == 4))
                {
                    play = false;
                    Follow2.play = true;
                    Follow2.Fileno += 1;
                    figures.SetActive(true);
                }
                if (name == "Woman" & (Fileno == 1 | Fileno == 2))
                {
                    play = false;
                    Follow1.play = true;
                  //  Follow1.Fileno += 1;
                }
                //wait after first language spoken
<<<<<<< HEAD
                if (Fileno > 0 & location!="DA")
=======
                if (Fileno > 0)
>>>>>>> 60f8430dcd9ac87f868378308a42102f1a76f35f
                {
                    next = false;
                   
                }
        //turn off previous
<<<<<<< HEAD
=======

                    Fileno += 1;
>>>>>>> 60f8430dcd9ac87f868378308a42102f1a76f35f

                    Fileno += 1;
                //not working
                //yield return new WaitForSeconds(1f);
                videoplayer.loopPointReached += CheckOver;

                //pdsend.sendMessagePD("3 1");

<<<<<<< HEAD

=======
                yield return new WaitUntil(() => !videoplayer.isPlaying);
              if (location!="DA")  text.text = "Press C to continue or R to repeat phrase";
>>>>>>> 60f8430dcd9ac87f868378308a42102f1a76f35f
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

