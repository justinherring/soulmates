using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        if (health > slider.maxValue)
        {
            slider.value = slider.maxValue;
        } else if (health < 0)
        {
            slider.value = 0;
        } else
        {
            slider.value = health;
        }
    }
}
