using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetMaxHealth(float _health)
    {
        slider.maxValue = _health;
        slider.value = _health;
    }
    
    public void SetHealth(float _health)
    {
        slider.value = _health;
    }
}
