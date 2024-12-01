using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI tmpro;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        tmpro.text = health.ToString() + "/" + slider.maxValue.ToString();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        tmpro.text = health.ToString() + "/" + slider.maxValue.ToString();
    }
}
