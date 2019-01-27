using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public static class UDPReceive
{
    // receiving Thread
    private static readonly Thread ReceiveThread = new Thread(ReceiveData);

    public const int Port = 60607;

    public static string SenderId;

    // udpclient object
    private static readonly UdpClient Client = new UdpClient(Port);

    public static readonly Queue<UDPEntity> ObjectQueue =
        new Queue<UDPEntity>();

    // init
    public static void Init(string senderId)
    {
        if (ReceiveThread.IsAlive) return;
        
        SenderId = senderId;
        ReceiveThread.IsBackground = true;
        ReceiveThread.Start();
    }

    // receive thread 
    private static void ReceiveData()
    {
        while (true)
        {
            try
            {
                // receive bytes
                var anyIp = new IPEndPoint(IPAddress.Any, 0);
                var data = Client.Receive(ref anyIp);
                var text = Encoding.UTF8.GetString(data);
                var serialObject = JsonUtility.FromJson<UDPEntity>(text);

                if (!serialObject.origin.Equals(SenderId))
                {
                    ObjectQueue.Enqueue(serialObject);
                }
            }
            catch
            {
            }
        }
    }
}