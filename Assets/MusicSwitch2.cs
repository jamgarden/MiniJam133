using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch2 : MonoBehaviour
{
    [SerializeField] private AudioSource world1Music;
    [SerializeField] private AudioSource world2Music;

    private void OnEnable()
    {
        world1Music.mute = false;
        world2Music.mute = true;
    }       
}
