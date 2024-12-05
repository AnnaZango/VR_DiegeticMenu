using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPortal : MonoBehaviour
{
    SceneController sceneController;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
    }   

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sceneController.LoadSceneByIndex(1);
        }
    }
}
