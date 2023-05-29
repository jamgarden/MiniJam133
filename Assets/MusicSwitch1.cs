using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch1 : MonoBehaviour
{
    [SerializeField] private AudioSource world1Music;
    [SerializeField] private AudioSource world2Music;
     private void OnEnable()
    {
        world1Music.mute = true;
        world2Music.mute = false;
    }       
}
