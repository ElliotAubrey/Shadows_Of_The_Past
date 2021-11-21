using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int perksRemaining = 7;
    public void DisablePerk(int perk)
    {
        if (perk == 1) mind.Disable();
        if (perk == 2) sight.Disable();
        if (perk == 3) hearing.Disable();
        if (perk == 4) steadyHand.Disable();
        if (perk == 5) vitality.Disable();
        if (perk == 6) movement.Disable();
        perksRemaining--;
        Debug.Log(perksRemaining);
        if (perksRemaining <= 0) gameOverScreen.SetActive(true);
        if (perksRemaining == 1) soulButton.SetActive(true);
    }
}
