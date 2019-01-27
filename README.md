# unity-udp-networking-sample
Simple UDP networking for simple interaction between games.

# Data structure

The object that is sent across the network is a simple json object:

```
{
  "origin": "me",
  "name": "SomeObjectOrEvent",
  "position": { "x": float, "y": float },
  "velocity": { "x": float, "y": float },
}
```

# Other implementations

See [Godot_UDP_Object_Network](https://github.com/fahrstuhl/Godot_UDP_Object_Network) for a Godot implementation of this protocol.
