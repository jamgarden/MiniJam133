using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftSwitch : MonoBehaviour, IPunchable
{
    //[SerializeField]
    //GameObject TimeShiftObject;

    TimeShifter timeShifter;
    // Start is called before the first frame update
    void Start()
    {

        timeShifter = FindObjectOfType<TimeShifter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Punch()
    {
        Debug.Log("Punched a TimeShift switch");
        timeShifter.ShiftTime();
    }
}
