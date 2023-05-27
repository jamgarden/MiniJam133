using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GreenBlock : MonoBehaviour
{

    [SerializeField]
    private string sceneTarget;
    

    // And check for when something on layer "Player" collides with it.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.layer == 8) // Layer 8 SHOULD be player
            {
                Debug.Log("Tagged player, ending game");
                if(sceneTarget != null)
                {
                    SceneManager.LoadScene(sceneTarget); // Make sure to set 
                } else
                {
                    Debug.LogWarning("Target Scene not specified on GreenBlock");
                }
            }
        }
    }
}
