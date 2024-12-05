using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class SwitchControls : MonoBehaviour
{
    [SerializeField] ActionBasedControllerManager controllerManager;
    //[SerializeField] DynamicMoveProvider dynamicMoveProvider;
    [SerializeField] ContinuousTurnProviderBase continuousTurnProvider;
    [SerializeField] SnapTurnProviderBase snapTurnProvider;

    [Header("Settings bufé")]
    [SerializeField] Image squareSmoothD;
    [SerializeField] Image squareTeleportD;
    [SerializeField] Image squareSmoothT;
    [SerializeField] Image squareSnapT;

    void Start()
    {
        SetBufeControlsToComfort();
    }

    
    void Update()
    {
        
    }

    private void SetBufeControlsToComfort()
    {
        squareSnapT.gameObject.SetActive(true);
        squareSmoothT.gameObject.SetActive(false); 
        squareSmoothD.gameObject.SetActive(false);
        squareTeleportD.gameObject.SetActive(true);
    }

    //private void ChangeTurnControls()
    //{
    //    bool isOn = controllerManager.smoothTurnEnabled;
    //    if (isOn)
    //    {
    //        controllerManager.smoothTurnEnabled = false;
    //        Debug.Log("Turn set to snap");
    //    }
    //    else
    //    {
    //        controllerManager.smoothTurnEnabled = true;
    //        Debug.Log("Turn set to continous");
    //    }
    //}

    public void SetTurnToSnap()
    {
        snapTurnProvider.enabled = true;
        continuousTurnProvider.enabled = false;
        squareSnapT.gameObject.SetActive(true);
        squareSmoothT.gameObject.SetActive(false);
    }

    public void SetTurnContinuous()
    {
        continuousTurnProvider.enabled = true;
        snapTurnProvider.enabled = false;
        squareSnapT.gameObject.SetActive(false);
        squareSmoothT.gameObject.SetActive(true);
    }

    public void SetContinuousMove()
    {
        controllerManager.smoothMotionEnabled = true;
        squareSmoothD.gameObject.SetActive(true);
        squareTeleportD.gameObject.SetActive(false);
    }

    public void SetTeleportationMove()
    {
        controllerManager.smoothMotionEnabled = false;
        squareSmoothD.gameObject.SetActive(false);
        squareTeleportD.gameObject.SetActive(true);
    }

    //private void ChangeMoveControls()
    //{
    //    bool isOn = controllerManager.smoothMotionEnabled;
    //    if (isOn)
    //    {
    //        controllerManager.smoothMotionEnabled = false;
    //        Debug.Log("Move set to discrete"); //ONLY TELEPORT SHOULD WORK
    //        //dynamicMoveProvider.enabled = false;
    //    }
    //    else
    //    {
    //        controllerManager.smoothMotionEnabled = true;
    //        Debug.Log("Move set to smooth"); //WE SHOULD MOVE WITH LEFT HAND
    //        //dynamicMoveProvider.enabled = true;
    //    }
    //}

}
