# unity-udp-networking-sample
Simple UDP networking for simple interaction between games.

## How it works
An object is json serialized and sent via UDP Broadcast to port 60607.

## How to use
All classes are static so you can easily access them from anywhere.  

### Receive data

To receive objects from other games initialize the `UDPReceiver`:

```
UDPReceive.Init("MyClientName");
```

There is no callback when new packages arrive. Use the `Update()` function to check for new packets instead:

```c#
void Update() 
{
  if (UDPReceive.objectQueue.Count > 0)
  {
    // do something with the queue
  }
}
```
### Send data

To send data to other games use the `UDPSend` class:

```c#
UDPSend.SendObject("PlayerLanded", _rigidbody.position, _rigidbody.velocity);
```

## Data structure

The object that is sent across the network is a simple json object:

```json
{
  "origin": "me",
  "name": "SomeObjectOrEvent",
  "position": { "x": float, "y": float },
  "velocity": { "x": float, "y": float },
}
```

## Other implementations

See [Godot_UDP_Object_Network](https://github.com/fahrstuhl/Godot_UDP_Object_Network) for a Godot implementation of this protocol.
