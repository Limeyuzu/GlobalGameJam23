using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{

    public Slider slider;
    public Gradient colorGradient;
    public Image fill;
    public Transform theCamera;

    public void SetMaxBarValue(int maxValue) 
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;

        fill.color = colorGradient.Evaluate(1f);
    }

    public void SetBarValue(int value)
    {
        slider.value = value;
        fill.color = colorGradient.Evaluate(slider.normalizedValue);
    }
    private void Update()
    {
        transform.LookAt(theCamera);
    }
}