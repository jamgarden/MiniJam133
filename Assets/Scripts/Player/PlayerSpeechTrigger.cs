using System;
using UnityEngine;

public class PlayerSpeechTrigger : MonoBehaviour
{
    public static Action<string> OnTriggerPlayerSpeech;

    [SerializeField] private string speechText;

    public void TriggerSpeech()
    {
        OnTriggerPlayerSpeech?.Invoke(speechText);
    }
}