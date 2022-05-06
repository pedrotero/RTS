using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Quaternion startRotation;
    public Image health;
    void Start()
    {
        health.color = Color.green;
        startRotation = transform.rotation;
    }
    void Update()
    {
        transform.rotation = startRotation;
    }

    public void UpdateDMG(float percleft)
    {
        health.fillAmount = percleft;
        health.color = Color.Lerp(Color.red, Color.green, percleft);
    }
}
