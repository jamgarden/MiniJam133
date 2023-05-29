using System;
using UnityEngine;

public class TimeShifter : MonoBehaviour
{
    [SerializeField] private GameObject presentGameObjects;
    [SerializeField] private GameObject futureGameObjects;


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

        }
        else
        {
            Debug.Log("In the present");

            presentGameObjects.SetActive(true);
            futureGameObjects.SetActive(false);
        }
    }

    [ContextMenu("Switch")]
    public void ShiftTime()
    {
        presentGameObjects.SetActive(!presentGameObjects.activeSelf);
        futureGameObjects.SetActive(!futureGameObjects.activeSelf);
        OnTimeShift?.Invoke();
    }
}