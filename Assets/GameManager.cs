using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;

public class GameManager:MonoBehaviour
{

    public MeterScript DemandingSlider; 
    public MeterScript GreenSlider;

    private PostProcessVolume ppV; // assign the post-processing volume in the inspector
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
        
    }

    public int demandingValue = 0;

  //  public int redValue = 0;

    public int greenValue = 0;
//    public int blueValue = 0;

    private void Start()
    {
        ppV = Camera.main.GetComponent<PostProcessVolume>();
        DemandingSlider.SetMaxHealth(100);
        GreenSlider.SetMaxHealth(100);
        DemandingSlider.SetHealth(demandingValue);
        GreenSlider.SetHealth(greenValue);
        //DemandingSlider.value = demandingValue;
        //GreenSlider.value = greenValue;

    }

    //private void changeWhite(int val)
    //{
    //    WhiteRate += 
    //}

    public void ChangeDemandingValue(int demandToAdd)
    {
        demandingValue += demandToAdd;
        DemandingSlider.SetHealth(demandingValue);

    }

    //public void ChangeRedValue(int redToAdd)
    //{
    //    redValue+= redToAdd;
    //    changeWhite(redToAdd / 3);
    //}

    //public void ChangeBlueValue(int blueToAdd)
    //{
    //    blueValue += blueToAdd;
    //    WhiteRate += blueToAdd / 3;
    //}
    public void ChangeGreenValue(int greenToAdd)
    {
        greenValue += greenToAdd;
        //WhiteRate += greenToAdd/ 3;
        GreenSlider.SetHealth(greenValue);
        ColorGrading cg;
        if (ppV.profile.TryGetSettings(out cg)) // get the bloom settings from the volume
        {
            cg.brightness.value += greenToAdd; // set the bloom intensity
        }
        cg.brightness.value = Math.Clamp(cg.brightness.value, -45, 30);
    }
}
