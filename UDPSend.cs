using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;

public static class UDPSend
{
    // "connection" things
    private static readonly IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Broadcast, UDPReceive.Port);
    private static readonly UdpClient Client = new UdpClient();

    // sendData
    public static void SendObject(string name, Vector2 position, Vector2 velocity)
    {
        var serialObject = new UDPEntity
        {
            origin = UDPReceive.SenderId,
            name = name,
            position = position,
            velocity = velocity
        };

        var jsonString = JsonUtility.ToJson(serialObject);

        try
        {
            var data = Encoding.UTF8.GetBytes(jsonString);
            Client.Send(data, data.Length, RemoteEndPoint);
        }
        catch
        {
        }
    }

    public static Vector3 CalculatePosition(Vector3 worldPosition, Camera camera)
    {
        var screenPosition = camera.WorldToScreenPoint(worldPosition);
        return screenPosition / camera.pixelWidth;
    }
}