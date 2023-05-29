using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour, IPunchable
{
    [SerializeField]
    string targetSegment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Punch()
    {
        Debug.Log("I just got punched, and I'm a door btw");
        SceneManager.LoadScene(targetSegment);
    }
}
