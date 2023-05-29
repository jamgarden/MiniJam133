using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;


    public bool inThePresent = true;

    public int score = 0; // Track player score

    // Awake is called before Start method
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameManager test = FindObjectOfType<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
