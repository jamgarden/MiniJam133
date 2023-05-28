using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject audioSourcePrefab;
    private ObjectPool<GameObject> audioSourcePool;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        audioSourcePool = new ObjectPool<GameObject>(
            () => Instantiate(audioSourcePrefab),
            audioSource => audioSource.SetActive(true),
            audioSource => audioSource.SetActive(false),
            audioSource => Destroy(audioSource));
    }

    public void PlaySound(AudioClip clip)
    {
        GameObject audioSourceGameObject = audioSourcePool.Get();
        AudioSource audioSource = audioSourceGameObject.GetComponent<AudioSource>();
        if (audioSourceGameObject != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(ReleaseAudioSource(clip.length, audioSourceGameObject));
        }
    }

    private IEnumerator ReleaseAudioSource(float clipLength, GameObject audioClipGameObject)
    {
        yield return new WaitForSeconds(clipLength);
        audioSourcePool.Release(audioClipGameObject);
    }
}