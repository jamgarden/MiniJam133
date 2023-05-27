using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerSpeechBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speechLabel;
    [SerializeField] private float delayPerCharacter;

    public void StartSpeech(string text)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayText(text));
    }

    private IEnumerator DisplayText(string text)
    {
        speechLabel.text = text;
        for (int i = 0; i < text.Length; i++)
        {
            speechLabel.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delayPerCharacter);
        }
    }
}