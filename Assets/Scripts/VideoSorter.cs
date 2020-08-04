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
    public AudioClip[] listAudio;
    public string[] dharug;
    public string[] translations;
    public VideoSorter Follow1, Follow2;
    public VideoClip[] listVideo;
    public string[] listOfVideos;
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
        if (level == stage.Dharug) level = stage.English;
        if (level == stage.None) level = stage.Dharug;
        if (level == stage.Start) level = stage.None;
        // level = stage.Dharug;
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
use video loaded
if (folder=="")
    url = System.IO.Path.Combine(Application.streamingAssetsPath, listOfVideos[0]);
else
       url = System.IO.Path.Combine(Application.streamingAssetsPath, folder, listOfVideos[0]);

      videoplayer.url = url;

#else
        videoplayer.clip = listVideo[Fileno];
        videoplayer.Prepare();
#endif
        videoplayer.Prepare();
        play = true;
        next = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
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
        if (name == "Man Sitting")
        {
            yield return new WaitForSeconds(7f);
            figures.SetActive(true);

        }


        // Move to next speaker
        if (name == "Magpie" & Fileno >= 0 & Fileno < 3)
        {

            if (Camera.main.transform.eulerAngles.y < 0 | Camera.main.transform.eulerAngles.y > 290)

                Camera.main.transform.Rotate(Vector3.up, 10 * Time.deltaTime);

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
        //stop looping for talking
        if (0 < Fileno | Fileno < listVideo.Length - 1) videoplayer.isLooping = false;
        else videoplayer.isLooping = true;
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
                videoplayer.Prepare();
        

#else
                videoplayer.clip = listVideo[Fileno];
     
       
#endif

                //play background sounds;
                if (Fileno==0) audiosource.Play();
                videoplayer.Play();


                
               
                //add end loop audio
                yield return new WaitForSeconds(1f);
                if (level == stage.Dharug) text.text = dharug[Fileno];
                else if (level == stage.English) text.text = translations[Fileno];
                else text.text = "";
                text.text = videoplayer.url.ToString();
                if (name == "Man Standing" | name == "Woman" | Fileno == listVideo.Length - 1)
                {

                    audiosource.Play();
                    yield return new WaitUntil(() => !audiosource.isPlaying);
                }
                if (name == "Man Standing" & (Fileno == 1 | Fileno == 2))
                {
                    play = false;
                    Follow1.play = true;
                    Follow1.Fileno += 1;
                }
                if (name == "Man Sitting" & (Fileno == 1 | Fileno == 2))
                {

                    play = false;
                    Follow1.play = true;
                    Follow1.Fileno += 1;
                }
                if (name == "Man Standing" & (Fileno == 3 | Fileno == 4))
                {
                    play = false;
                    Follow2.play = true;
                    Follow2.Fileno += 1;
                }
                if (name == "Woman" & (Fileno == 1 | Fileno == 2))
                {
                    play = false;
                    Follow1.play = true;
                    Follow1.Fileno += 1;
                }
                //wait after first language spoken
                if (Fileno > 0) next = false;

                Fileno += 1;

                //not working
                //yield return new WaitForSeconds(1f);
                videoplayer.loopPointReached += CheckOver;
                //pdsend.sendMessagePD("3 1");

                yield return new WaitUntil(() => !videoplayer.isPlaying);
                //  yield return new WaitForSeconds((float)videoplayer.length);


            }

        }


    }
    //seems to work
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        change = true;
        //Debug.Log("Changeing");

    }


}

