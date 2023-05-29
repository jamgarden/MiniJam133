using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicSwitch1 : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot world1Snap;
    [SerializeField] private AudioMixerSnapshot world2Snap;


    private void OnEnable()
    {
        world1Snap.TransitionTo(0.01f);
    }       
}
