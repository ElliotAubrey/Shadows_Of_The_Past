using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkDisabler : MonoBehaviour
{
    [SerializeField] Mind mind;
    [SerializeField] Sight sight;
    [SerializeField] Hearing hearing;
    [SerializeField] SteadyHand steadyHand;
    [SerializeField] Vitality vitality;
    [SerializeField] Movement movement;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject soulButton;
    [SerializeField] Image bodyImageSoul;
    [SerializeField] TextMeshProUGUI text;

    int perksRemaining = 7;
    float alpha = 0;
    float decValue = 255 / 6;
    Color newColor;
    private void Awake()
    {
        newColor = bodyImageSoul.color;
    }
    public void DisablePerk(int perk)
    {
        if (perk == 1) mind.Disable();
        if (perk == 2) sight.Disable();
        if (perk == 3) hearing.Disable();
        if (perk == 4) steadyHand.Disable();
        if (perk == 5) vitality.Disable();
        if (perk == 6) movement.Disable();
        perksRemaining--;
        if (perksRemaining <= 0) gameOverScreen.SetActive(true);
        if (perksRemaining == 1)
        {
            soulButton.SetActive(true);
            text.text = "With nothing more to lose,\n your soul is the only sacrifice. \n Your broken mind can fathom to submit…";
        }
        alpha += decValue;
        newColor.a = alpha/255;
        bodyImageSoul.color = newColor;
    }
}
