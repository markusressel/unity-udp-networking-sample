using System.Collections.Generic;
using UnityEngine;

using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class UDPReceive {

    // receiving Thread
    static Thread receiveThread = new Thread(ReceiveData);

    public static int port = 60607;

    public static string SenderId;

    // udpclient object
    static UdpClient client = new UdpClient(port);

    public static Queue<UDPEntity> objectQueue =
        new Queue<UDPEntity>();

    // init
    public static void Init(string senderId)
    {
        SenderId = senderId;
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // receive thread 
    private static void ReceiveData()
    {
        while (true)
        {
            try
            {
                // Bytes empfangen.
                var anyIp = new IPEndPoint(IPAddress.Any, 0);
                var data = client.Receive(ref anyIp);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
                var text = Encoding.UTF8.GetString(data);
                var serialObject = JsonUtility.FromJson<UDPEntity>(text);

                if (!serialObject.origin.Equals(SenderId))
                {
                    objectQueue.Enqueue(serialObject);
                    //Debug.Log(serialObject.name);
                }
            }
            catch
            {

            }
        }
    }
}
