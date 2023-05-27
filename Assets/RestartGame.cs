using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    [SerializeField]
    private string sceneTarget;
    public void Activate()
    {
        Debug.Log("restarting gameplay level");
        if(sceneTarget != null)
        {
            SceneManager.LoadScene(sceneTarget); 
        } else
        {
            Debug.LogWarning("The target scene has not been set on the Restart Button");
        }

    }
}
