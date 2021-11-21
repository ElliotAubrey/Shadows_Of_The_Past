using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] Volume vol;
    [SerializeField] LayerMask sanityLossMaskBase, sanityLossMaskShadowMan;
    [SerializeField] float effectRadius = 5f;
    [SerializeField] float effectRange = 10f;
    [SerializeField] float lossRate, gainRate;
    [SerializeField] TextMeshProUGUI sanity;
    [SerializeField] Slider sanitySlider;
    [SerializeField] HealthSystem playerHealth;

    float insanity = 0f;
    float lossAmount, gainAmount;
    FilmGrain grain;
    ColorAdjustments ajustments;
    InsanitySound iSound;

    bool loseSanity = false;
    int multiplier = 1;
    int modifier = 1;

    private void Awake()
    {
        iSound = FindObjectOfType<InsanitySound>();
        lossAmount = lossRate / 50;
        gainAmount = gainRate / 50;
        vol.profile.TryGet(out grain);
        vol.profile.TryGet(out ajustments);
        if (!grain)
        {
            vol.profile.Add<FilmGrain>();
            vol.profile.TryGet(out grain);
        }
        if (!ajustments)
        {
            vol.profile.Add<ColorAdjustments>();
            vol.profile.TryGet(out ajustments);
        }

    }

    private void FixedUpdate()
    {
        /*
        if((Physics2D.CircleCast(transform.position, effectRadius, Vector2.up, effectRange, sanityLossMaskShadowMan) || Physics2D.CircleCast(transform.position, effectRadius, Vector2.up, -effectRange, sanityLossMaskShadowMan)))
        {
            multiplier = 2;
            loseSanity = true;
            Debug.Log("Lose Sanity Shadow");
        }
        if (Physics2D.CircleCast(transform.position, effectRadius, Vector2.up, effectRange, sanityLossMaskBase) || Physics2D.CircleCast(transform.position, effectRadius, Vector2.up, -effectRange, sanityLossMaskBase))
        {
            multiplier = 1;
            loseSanity = true;
            Debug.Log("Lose Sanity Base");
        }
        */

        if (loseSanity) LoseSanity(multiplier);
        else RegenSanity();

        float damage = 0;
        if (insanity > 75)
        {
            damage = 0.2f / 50;
            if (insanity > 80) damage = 0.4f / 50;
            if (insanity > 85) damage = 0.8f / 50;
            if (insanity > 90) damage = 1.6f / 50;
            if (insanity > 95) damage = 3.2f / 50;
            playerHealth.TakeDamage(damage);
        }

        iSound.insanity = insanity;
    }

    private void RegenSanity()
    {
        insanity -= gainAmount;
        insanity = Mathf.Clamp(insanity, 0, 100);
        grain.intensity.value = insanity / 100;
        ajustments.saturation.value = -insanity;
        sanity.text = insanity.ToString();
        sanitySlider.value = insanity * -1;
    }

    private void LoseSanity(int multiplier)
    {
        insanity += lossAmount * multiplier * modifier ;
        insanity = Mathf.Clamp(insanity, 0, 100);
        grain.intensity.value = insanity / 100;
        ajustments.saturation.value = -insanity;
        sanity.text = insanity.ToString();
        sanitySlider.value = insanity * -1;
        loseSanity = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("BaseEnemy"))
        {
            loseSanity = true;
            multiplier = 1;
        }
        if (collision.CompareTag("ShadowEnemy"))
        {
            loseSanity = true;
            multiplier = 2;
        }
    }

    public void SetModifier(int newMod)
    {
        modifier = newMod;
    }

    public void SetInsanity(float value)
    {
        insanity = value;
    }
} 
