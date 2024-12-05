using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class SettingsTest : MonoBehaviour
{
    [Header("Interactable elements")]
    [SerializeField] XRSlider sliderLight;
    [SerializeField] XRKnob dialSubtitles; //discrete
    [SerializeField] XRSlider sliderMusic;
    [SerializeField] XRSlider sliderSound;
    [SerializeField] XRKnob wheelSpeed;
    [SerializeField] XRKnob dialParticles;
    [SerializeField] XRKnob dialDifficulty;
    [SerializeField] XRKnob dialMood;

    [Header("Elements to modify")]
    //[SerializeField] XRDeviceSimulator simulator;
    [SerializeField] ContinuousMoveProviderBase continuousMoveProvider;
    [SerializeField] Light directionalLight;
    //[SerializeField] GameObject objectToRotateWheel;
    [SerializeField] GameObject objectToScaleDial;
    [SerializeField] AudioSource musicAudio;
    [SerializeField] AudioSource soundAudio;
    [SerializeField] GameObject bubbleText;
    [SerializeField] GameObject[] particlesArray;
    [SerializeField] GameObject[] particlesPotArray;
    [SerializeField] GameObject[] chilisDifficulty;
    [SerializeField] GameObject[] postProcessingVolumes; //0 default, 1 pale, 2 bnw

    //Settings bufé
    [Header("Settings menu")]
    [SerializeField] Image[] squareDialogueSizesOrdered;
    [SerializeField] Image[] squareMoodOrdered; //0 default, 1 pale, 2 bnw
    [SerializeField] Image[] squareParticlesSizesOrdered;
    [SerializeField] Image[] squareDifficultyOrdered;
    [SerializeField] Image speedImage;
    [SerializeField] Image volumeSoundImage;
    [SerializeField] Image volumeMusicImage;
    [SerializeField] Image brightnessImage;
    [SerializeField] float[] sizesText;

    int currentValueSliderParticles = -1;
    int currentValueSliderMood = -1;

    void Start()
    {
        ChangeLightIntensitySlider();
        ChangeSpeedContinuousMovement();

        ChangeMusicVolume();
        ChangeSoundVolume();
        ChangeSubtitleSize();
        SetParticleAmount();
        ChangeMood();
    }

    
    void Update()
    {
        
    }

    public void ChangeLightIntensitySlider()
    {
        directionalLight.intensity = sliderLight.value * 1.5f;
        brightnessImage.fillAmount = sliderLight.value;
    }


    public void ChangeMusicVolume()
    {
        musicAudio.volume = sliderMusic.value;
        volumeMusicImage.fillAmount = sliderMusic.value;
    }

    public void ChangeSoundVolume()
    {
        soundAudio.volume = sliderSound.value;
        volumeSoundImage.fillAmount = sliderSound.value;
    }

    public void ChangeSubtitleSize()
    {
        float valueDialMultiplied = dialSubtitles.value * 100;
        int valueDialInt = Mathf.RoundToInt(valueDialMultiplied);
        
        switch(valueDialInt)
        {
            case 0:
                ShowSquareImageByIndex(squareDialogueSizesOrdered, 0);
                bubbleText.transform.localScale = new Vector3(sizesText[0], sizesText[0], sizesText[0]); 
                break;
            case 33:
                ShowSquareImageByIndex(squareDialogueSizesOrdered, 1);
                bubbleText.transform.localScale = new Vector3(sizesText[1], sizesText[1], sizesText[1]);
                break;
            case 67:
                ShowSquareImageByIndex(squareDialogueSizesOrdered, 2);
                bubbleText.transform.localScale = new Vector3(sizesText[2], sizesText[2], sizesText[2]);
                break;
            case 100:
                ShowSquareImageByIndex(squareDialogueSizesOrdered, 3);
                bubbleText.transform.localScale = new Vector3(sizesText[3], sizesText[3], sizesText[3]);
                break;
            default: Debug.Log("Dial value not defined!"); break;
        }                  
    }

    

    public void ChangeSpeedContinuousMovement()
    {
        continuousMoveProvider.moveSpeed = Mathf.Lerp(0.2f, 2, wheelSpeed.value);
        speedImage.fillAmount = wheelSpeed.value;
    }

    public void SetParticleAmount()
    {
        float valueDialMultiplied = dialParticles.value * 100;
        int valueDialInt = Mathf.RoundToInt(valueDialMultiplied);

        if(currentValueSliderParticles == valueDialInt) { return; }

        currentValueSliderParticles = valueDialInt;

        switch (valueDialInt)
        {
            case 0:
                ShowSquareImageByIndex(squareParticlesSizesOrdered, 0);
                DisableAllParticles();
                break;
            case 50:
                ShowSquareImageByIndex(squareParticlesSizesOrdered, 1);
                DisableAllParticles();
                ShowParticles(0);
                break;            
            case 100:
                ShowSquareImageByIndex(squareParticlesSizesOrdered, 2);
                DisableAllParticles();
                ShowParticles(1);
                break;
            default: Debug.LogWarning("Dial value not defined!"); break;
        }
    }

    private void DisableAllParticles()
    {
        foreach (GameObject particleSystem in particlesArray)
        {
            particleSystem.SetActive(false);
        }
        foreach (GameObject particleSystem in particlesPotArray)
        {
            particleSystem.SetActive(false);
        }
    }

    private void ShowParticles(int index)
    {
        particlesArray[index].gameObject.SetActive(true);
        particlesPotArray[index].gameObject.SetActive(true);
    }

    public void SetDifficulty()
    {
        float valueDialMultiplied = dialDifficulty.value * 100;
        int valueDialInt = Mathf.RoundToInt(valueDialMultiplied);

        switch (valueDialInt)
        {
            case 0:
                ShowSquareImageByIndex(squareDifficultyOrdered, 0);
                ShowChilis(0);
                break;
            case 50:
                ShowSquareImageByIndex(squareDifficultyOrdered, 1);
                ShowChilis(1);
                break;
            case 100:
                ShowSquareImageByIndex(squareDifficultyOrdered, 2);
                ShowChilis(2);
                break;
            default: Debug.LogWarning("Dial value not defined!"); break;
        }
    }

    private void ShowChilis(int indexShow)
    {
        foreach(GameObject chilis in chilisDifficulty)
        {
            chilis.SetActive(false);
        }        
        chilisDifficulty[indexShow].gameObject.SetActive(true);
    }

    public void ChangeMood()
    {
        float valueDialMultiplied = dialMood.value * 100;
        int valueDialInt = Mathf.RoundToInt(valueDialMultiplied);

        if (currentValueSliderMood == valueDialInt) { return; }

        currentValueSliderMood = valueDialInt;

        switch (valueDialInt)
        {
            case 0:
                ShowSquareImageByIndex(squareMoodOrdered, 0);
                EnableVolumeByIndex(0);
                break;
            case 50:
                ShowSquareImageByIndex(squareMoodOrdered, 1);
                EnableVolumeByIndex(1);
                break;
            case 100:
                ShowSquareImageByIndex(squareMoodOrdered, 2);
                EnableVolumeByIndex(2);
                break;
            default: Debug.LogWarning("Dial value not defined!"); break;
        }
    }

    private void EnableVolumeByIndex(int index)
    {
        foreach (GameObject volume in postProcessingVolumes)
        {
            volume.SetActive(false);
        }
        postProcessingVolumes[index].gameObject.SetActive(true);
    }
    
    private void ShowSquareImageByIndex(Image[] images, int index)
    {
        foreach (Image image in images)
        {
            image.gameObject.SetActive(false);
        }
        images[index].gameObject.SetActive(true);
    }

}
