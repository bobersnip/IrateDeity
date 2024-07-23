using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SimpleRandomWalkObject : ScriptableObject
{
    public int iterations = 10, walkLength = 100;
    public bool startRandomlyEachIteration = true;
}