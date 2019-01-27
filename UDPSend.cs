using UnityEngine;

using System.Text;
using System.Net;
using System.Net.Sockets;

public static class UDPSend {

    // "connection" things
    private static IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, UDPReceive.port);
    private static UdpClient client = new UdpClient();

    // sendData
    public static void SendObject(string name, Vector2 position, Vector2 velocity)
    {
        var serialObject = new UDPEntity();
        serialObject.origin = UDPReceive.SenderId;
        serialObject.name = name;
        serialObject.position = position;
        serialObject.velocity = velocity;

        var jsonString = JsonUtility.ToJson(serialObject);

        try
        {
            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            var data = Encoding.UTF8.GetBytes(jsonString);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch
        {
        }
    }
}
