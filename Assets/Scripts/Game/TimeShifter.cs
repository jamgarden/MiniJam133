using System;
using UnityEngine;

public class TimeShifter : MonoBehaviour
{
    [SerializeField] private GameObject presentGameObjects;
    [SerializeField] private GameObject futureGameObjects;

    public static Action OnTimeShift;

    private void Awake()
    {
        presentGameObjects.SetActive(true);
        futureGameObjects.SetActive(false);
    }

    [ContextMenu("Switch")]
    public void ShiftTime()
    {
        presentGameObjects.SetActive(!presentGameObjects.activeSelf);
        futureGameObjects.SetActive(!futureGameObjects.activeSelf);
        OnTimeShift?.Invoke();
    }
}