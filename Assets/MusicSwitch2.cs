using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;

public class MusicSwitch2 : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot world1Snap;
    [SerializeField] private AudioMixerSnapshot world2Snap;


    private void OnEnable()
    {
        world2Snap.TransitionTo(0.01f);
    }       
}
