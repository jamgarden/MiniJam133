using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput 
{
    public float Horizontal { get; }
    public bool Jump { get; }
    public bool Punch { get; }
}