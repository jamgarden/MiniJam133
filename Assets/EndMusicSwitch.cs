using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EndMusicSwitch : MonoBehaviour
{
[SerializeField] private AudioMixerSnapshot endScene;


    private void Start()
    {
        endScene.TransitionTo(0.01f);
    }       
    
}
