using System;
using UnityEngine;

public class TimeShifter : MonoBehaviour
{
    [SerializeField] private GameObject presentGameObjects;
    [SerializeField] private GameObject futureGameObjects;
    [SerializeField] private GameObject presentMusic;
    [SerializeField] private GameObject futureMusic;
    [SerializeField] private AudioSource shiftSound1;
    [SerializeField] private AudioSource shiftSound2;

    private bool JustPlayed = false;
    private GameManager gameManager;

    public static Action OnTimeShift;

    private void Awake()
    {
        if(presentGameObjects == null)
        {
            presentGameObjects = GameObject.FindGameObjectWithTag("PastWorld");
            Debug.Log(presentGameObjects);
        }
        if(futureGameObjects == null)
        {
            futureGameObjects = GameObject.FindGameObjectWithTag("PresentWorld");
            Debug.Log(futureGameObjects);
        }
        
    }
    private void Start()
    {
        GameManager[] objectArr = FindObjectsOfType<GameManager>();
        if (objectArr[0].inThePresent)
        {
            Debug.Log("In the past");

            
            presentGameObjects.SetActive(false);
            futureGameObjects.SetActive(true);
            presentMusic.SetActive(false);
            futureMusic.SetActive(true);

        }
        else
        {
            Debug.Log("In the present");

            //shiftSound1.Play();
            presentGameObjects.SetActive(true);
            futureGameObjects.SetActive(false);
        }
    }

    [ContextMenu("Switch")]
    public void ShiftTime()
    {   
        if (JustPlayed == false)
        {
            shiftSound2.Play();
            JustPlayed = true;
        }
        else 
        {
            shiftSound1.Play();
            JustPlayed = false;
        }
        presentGameObjects.SetActive(!presentGameObjects.activeSelf);
        futureGameObjects.SetActive(!futureGameObjects.activeSelf);
        OnTimeShift?.Invoke();
    }
}