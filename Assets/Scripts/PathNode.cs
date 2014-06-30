using UnityEngine;
using System.Collections;


public class PathNode:ScriptableObject
    {
    public float fValue = 0.0f;
    public float gValue = 0.0f;
    public float hValue = 0.0f;
    public PathNode parentPathNode;
    public Waypoint waypoint;


    }
