using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] GameObject gunHUD;
    [SerializeField] GameObject lanternHud;
    [SerializeField] PolygonCollider2D[] lanternColliders;
    [SerializeField] Light2D lanternLight;
    [SerializeField] TextMeshProUGUI revolverText, lanternText;
    [SerializeField] Color equiped, unequiped;

    public bool canChange = true;

    PlayerGun gunS;
    PlayerLantern lanternS;
    private void Awake()
    {
        gunS = GameObject.FindObjectOfType<PlayerGun>();
        lanternS = GameObject.FindObjectOfType<PlayerLantern>();
        Equip(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gunS.collected && canChange)
        {
            Equip(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && lanternS.collected && canChange)
        {
            Equip(2);
        }

    }

    void Equip(int num)
    {
        if (num == 1)
        {
            revolverText.color = equiped;
            lanternText.color = unequiped;
            lanternLight.enabled = false;
            lanternColliders[0].gameObject.SetActive(false);
            lanternColliders[1].gameObject.SetActive(false);
            gunS.enabled = true;
            lanternS.enabled = false;
            gunHUD.SetActive(true);
            lanternHud.SetActive(false);
        }
        if (num == 2)
        {
            revolverText.color = unequiped;
            lanternText.color = equiped;
            lanternLight.enabled = true;
            lanternColliders[0].gameObject.SetActive(false);
            lanternColliders[1].gameObject.SetActive(false);
            gunS.enabled = false;
            lanternS.enabled = true;
            gunHUD.SetActive(false);
            lanternHud.SetActive(true);
        }
    }
}


