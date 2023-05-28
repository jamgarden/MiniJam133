using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animCoupler : MonoBehaviour
{
    AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }
    public void TestFunc()
    {
        Debug.Log("Hello world, I am the pitter patter of tiny protag feet");
        aSource.Play();
    }
}
