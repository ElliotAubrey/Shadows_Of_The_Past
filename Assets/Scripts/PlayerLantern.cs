using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using UnityEngine.UI;

public class PlayerLantern : MonoBehaviour
{
    [SerializeField] List<PolygonCollider2D> lanternColliders;
    [SerializeField] Light2D lanternLight;
    [SerializeField] float low = 2;
    [SerializeField] float high = 8;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] int spareFuel = 1;
    [SerializeField] float lossRate = 0.1f;
    [SerializeField] Sprite[] fuelSprites;
    [SerializeField] Image fuelImage;
    [SerializeField] TextMeshProUGUI spareFuelText;

    public bool collected = false;

    float fuel;
    bool highIntensity = false;
    float lossAmount;


    private void Awake()
    {
        fuel = maxFuel;
        lossAmount = lossRate / 50;
        spareFuelText.text = spareFuel.ToString();
    }
    private void OnEnable()
    {
        IntensityInit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FlipIntensity();
        }
        if(fuel <= 80) fuelImage.sprite = fuelSprites[1];
        else if(fuel <= 50) fuelImage.sprite = fuelSprites[2];
        else if(fuel <= 20) fuelImage.sprite = fuelSprites[3];
        else if(fuel == 0) fuelImage.sprite = fuelSprites[4];
        else fuelImage.sprite = fuelSprites[0];
        if (fuel <= 0)
        {
            if (spareFuel > 0)
            {
                fuel = 100;
                spareFuel--;
                spareFuelText.text = spareFuel.ToString();
            }
            else
            {
                lanternLight.intensity = 0;
            }
        }
    }

    private void FlipIntensity()
    {
        if(highIntensity && fuel > 0)
        {
            highIntensity = false;
            lanternLight.intensity = low;
            lanternLight.pointLightInnerRadius = 5;
            lanternLight.pointLightOuterRadius = 12;
            lanternColliders[0].gameObject.SetActive(true);
            lanternColliders[1].gameObject.SetActive(false);
        }
        else if(fuel > 0)
        {
            highIntensity = true;
            lanternLight.intensity = high;
            lanternLight.pointLightInnerRadius = 8;
            lanternLight.pointLightOuterRadius = 15;
            lanternColliders[0].gameObject.SetActive(false);
            lanternColliders[1].gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        fuel -= lossAmount * lanternLight.intensity;
    }

    void IntensityInit()
    {
        if(highIntensity)
        {
            lanternLight.intensity = high;
            lanternLight.pointLightInnerRadius = 8;
            lanternLight.pointLightOuterRadius = 15;
            lanternColliders[0].gameObject.SetActive(false);
            lanternColliders[1].gameObject.SetActive(true);
        }
        else
        {
            lanternLight.intensity = low;
            lanternLight.pointLightInnerRadius = 5;
            lanternLight.pointLightOuterRadius = 12;
            lanternColliders[0].gameObject.SetActive(true);
            lanternColliders[1].gameObject.SetActive(false);
        }
    }

    public void AddFuel(int amount)
    {
        spareFuel += amount;
        spareFuelText.text = spareFuel.ToString();
    }
}
