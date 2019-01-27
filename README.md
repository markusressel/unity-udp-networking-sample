# unity-udp-networking-sample
Simple UDP networking for simple interaction between games.

## How it works
An object is json serialized and sent via UDP Broadcast to port 60607.

## How to use
All classes are static so you can easily access them from anywhere.  

### Receive data

To receive objects from other games initialize the `UDPReceiver` **once** in the `Start()` method of a global script:

```
UDPReceive.Init("MyClientName");
```

There is no callback when new packages arrive. Use the `Update()` function to check for new packets instead:

```c#
void Update() 
{
  if (UDPReceive.ObjectQueue.Count > 0)
  {
    // do something with the queue
  }
}
```
### Send data

To send data to other games use the `UDPSend` class:

```c#
// remember to always use this helper method to scale your
// world position to resolution independent screen coordinates (percentage values effectively)
Vector3 position = UDPSend.CalculatePosition(_rigidbody.position, Camera.main);
var velocity = _rigidbody.velocity;
UDPSend.SendObject("PlayerLanded", position, velocity);
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
The **origin** is an identifier for the game client.

**name** is used to specify the name/type of event that happened.

The **position** should contain values in `[0..1]` range (although this is not guaranteed) and may contain a `z` component when used in 3D context.

The **velocity** is mainly used to calculate the direction of the object. It's values are not limited to any range.

## Other implementations

See [Godot_UDP_Object_Network](https://github.com/fahrstuhl/Godot_UDP_Object_Network) for a Godot implementation of this protocol.
