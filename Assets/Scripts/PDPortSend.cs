using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class PDPortSend : MonoBehaviour
{
    #region private members 	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;
    #endregion
    // Use this for initialization
    public Camera cam;
    public GameObject box;
    int width;
    AudioClip soundfile;
        AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        //Output the current screen window width in the console
        width = Screen.width;
       
        socketConnection = new TcpClient("localhost", 9000);
      // do in intro
        //ConnectToTcpServer();
      //  sendMessagePD("1 1");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //get degress
            float degrees;

            Vector3 position = transform.position;
            position.y = 0;

            Vector3 targetPos = box.transform.position;
            targetPos.y = 0;

            Vector3 toOther = (position - targetPos).normalized;

            float angle = Mathf.Atan2(toOther.z, toOther.x) * Mathf.Rad2Deg + 180;
            float angleOpt = atan2Approximation(toOther.z, toOther.x) * Mathf.Rad2Deg + 180;

            Debug.DrawLine(position, targetPos, Color.yellow);
            sendMessage(angle);
            //ListenForData();
        }
    }
    /// <summary> 	
    /// Setup socket connection. 	
    /// </summary> 	
    private void ConnectToTcpServer()
    {
       
        try
        {
            
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
    /// <summary> 	
    /// Runs in background clientReceiveThread; Listens for incomming data. 	
    /// </summary>     
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient("localhost", 9000);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.ASCII.GetString(incommingData);
                        Debug.Log("message from server received as: " + serverMessage);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    /// <summary> 	
    /// Send message to server using socket connection. 	
    /// </summary> 	
    private void sendMessage( float deg)
    {
        Debug.Log("Starting");
        Vector3 screenPos = cam.WorldToScreenPoint(gameObject.transform.position);
        Debug.Log("target is " + screenPos.x + " pixels from the left");
        //Debug.Log("Screen Width : " + width);
       // screenPos = cam2.WorldToScreenPoint(gameObject.transform.position);
      //  Debug.Log("target is " + screenPos.x + " pixels from the camera");
       
        sound = GetComponent<AudioSource>();
        soundfile=sound.clip;
        int spread=10;
       
        Debug.Log("command is: pd group 1, spread " + spread );
        Debug.Log( "file " +soundfile.name.Substring(0,2) +" degrees " + deg);
       // string message= soundfile.name ;
        
        string message= soundfile.name.Substring(0, 2) +" "+deg+" "+spread;
        sendMessagePD(message+";");
    }
        
    public void sendMessagePD(string clientMessage)
        {

       
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
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message "+clientMessage+";");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
    float atan2Approximation(float y, float x) // http://http.developer.nvidia.com/Cg/atan2.html
    {
        float t0, t1, t2, t3, t4;
        t3 = Mathf.Abs(x);
        t1 = Mathf.Abs(y);
        t0 = Mathf.Max(t3, t1);
        t1 = Mathf.Min(t3, t1);
        t3 = 1f / t0;
        t3 = t1 * t3;
        t4 = t3 * t3;
        t0 = -0.013480470f;
        t0 = t0 * t4 + 0.057477314f;
        t0 = t0 * t4 - 0.121239071f;
        t0 = t0 * t4 + 0.195635925f;
        t0 = t0 * t4 - 0.332994597f;
        t0 = t0 * t4 + 0.999995630f;
        t3 = t0 * t3;
        t3 = (Mathf.Abs(y) > Mathf.Abs(x)) ? 1.570796327f - t3 : t3;
        t3 = (x < 0) ? 3.141592654f - t3 : t3;
        t3 = (y < 0) ? -t3 : t3;
        return t3;
    }


}
