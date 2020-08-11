using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

public class IntroText : MonoBehaviour
{
    #region private members
    public UnityEngine.UI.Text Text;
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    AudioSource audiosource;
    AudioClip audioclip;
    bool replay = true;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        audiosource = transform.GetComponent<AudioSource>();
        ConnectToTcpServer();
        sendMessage("1 1");
        Text.text= "This Language Learning Game was developed with\n   Dharug Teacher: Richard Green\nAnimations and graphics:\n    Jalmara Town    Josh Yasserie\n Erin Topfer Genevieve Stewart\n        Deborah Szapiro\nGame Developers: \n     Zac Casimatis   Cat Kutay\n Jaime Garcia    William Raffe\n";


        StartCoroutine(RunAudio());
      
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            replay = false;
            SceneManager.LoadScene("StartHarbour");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //rerun
            StartCoroutine(RunAudio());
        }
       
    }
    private void ConnectToTcpServer()
    {

        try
        {
          //  socketConnection = new TcpClient("localhost", 9000);


            //    clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            //    clientReceiveThread.IsBackground = true;
            //   clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    IEnumerator RunAudio()
    {
    
       if (audiosource.clip){
           audiosource.Play();
        while (audiosource.isPlaying)
        {
            // do nothing and keep returning while audio is still playing
            yield return new WaitForSeconds(audiosource.clip.length);
        }
       }
        //  sendMessage("1 2");
       
        Text.text = "\n\n\n\n\n\n\n\nPress R to replay; C for next Scene";
        if (!replay) SceneManager.LoadScene("StartHarbour");
    }
   

    private void sendMessage(string clientMessage)
    {

  Debug.Log(clientMessage+";");
        if (socketConnection == null)
        {

            Debug.Log("No connection");
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {

                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage+";");
                // Write byte array to socketConnection stream.                 
          // put back for PD
                //stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message " + clientMessageAsByteArray);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}