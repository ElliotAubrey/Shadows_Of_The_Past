using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerkManager : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject soulButton;
    [SerializeField] PlayerController controller;
    [SerializeField] PlayerGun gun;

    int perksRemaining = 8;
    PlayerSanity sanity;
    PlayerController pc;

    private void Awake()
    {
        sanity = FindObjectOfType<PlayerSanity>();
        pc = FindObjectOfType<PlayerController>();
    }
    public void EnablePerk(IPerk perk)
    {
        perk.Enable();
        perksRemaining++;
    }

    public void DisablePerk(IPerk perk)
    {
        perk.Disable();
    }

    public void Death()
    {
        deathScreen.gameObject.SetActive(true);
        controller.canMove = false;
        gun.canFire = false;
    }

    public void ChoiceMade()
    {
        deathScreen.SetActive(false);
        controller.canMove = true;
        sanity.SetInsanity(0);
        pc.SetStamina(100);
        StartCoroutine("EnableGun");
    }

    IEnumerator EnableGun()
    {
        yield return new WaitForSeconds(0.5f);
        gun.canFire = true;
        Debug.Log(gun.canFire);
    }
}
