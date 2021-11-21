using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using FMODUnity;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] float damage = 75f;
    [SerializeField] int magCapasity = 6;
    [SerializeField] int spareAmmo = 0;
    public float accuracy = 0.2f;
    [SerializeField] float rpm = 200;
    [SerializeField] TextMeshProUGUI ammoText, spareAmmoText;
    [SerializeField] LayerMask shootMask;
    [SerializeField] GameObject shootRenderer;
    [SerializeField] Texture2D cursor;
    [SerializeField] Transform firePoint;
    [SerializeField] Light2D muzzleFlash;
    [SerializeField] Animator controller;
    [SerializeField] StudioEventEmitter gunshot, gunclick, reload;

    public bool canFire = true;
    public bool reloading = false;
    public bool collected = false;

    float nextFireTime = 0f;
    float fireInterval;
    int ammo;
    float accuracyBase;

    Camera cam;
    PlayerWeaponManager PWM;
    PlayerController pController;
   

    private void Awake()
    {
        ammo = magCapasity;
        ammoText.text = ammo.ToString();
        spareAmmoText.text = spareAmmo.ToString();
        fireInterval = 1 / (rpm / 60);
        cam = Camera.main;
        PWM = FindObjectOfType<PlayerWeaponManager>();
        pController = FindObjectOfType<PlayerController>();
        accuracyBase = accuracy;
        Cursor.visible = false;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    private void OnEnable()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && nextFireTime <= Time.time && canFire && !controller.GetBool("IsMoving")) Fire();

        if (Input.GetKeyDown(KeyCode.R) && ammo < magCapasity && spareAmmo > 0 && !reloading && !controller.GetBool("IsMoving"))
        {
            StartReload();
            pController.canMove = false;
        }

        if (Input.GetKeyDown(KeyCode.M)) controller.StopPlayback();

        if (reloading && Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.R)) reloading = false;

    }
    public void PickUpAmmo(int amount)
    {
        spareAmmo += amount;
        spareAmmoText.text = spareAmmo.ToString();
    }

    void Fire()
    {
        if(ammo >= 1)
        {
            //firecode
            ammo--;
            ammoText.text = ammo.ToString();
            RaycastHit2D hit;
            Vector2 dir =  cam.ScreenToWorldPoint(Input.mousePosition) + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy)) - transform.position;
            hit = Physics2D.Raycast(transform.position, dir, 100, shootMask);
            if(hit.collider.TryGetComponent<HealthSystem>(out HealthSystem x))
            {
                x.TakeDamage(damage);
            }
            GameObject y = Instantiate(shootRenderer);
            LineRenderer lr = y.GetComponent<LineRenderer>();
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hit.point);
            StartCoroutine("DestroyLineRenderer", lr);
            //Play gunshot
            if (gunshot.enabled) gunshot.enabled = false;
            gunshot.enabled = true;
            StartCoroutine("MuzzleFlash");
        }
        else
        {
            //gun go click
            Debug.Log("Click");
            if (gunclick.enabled) gunclick.enabled = false;
            gunclick.enabled = true;
        }
        nextFireTime = Time.time + fireInterval;
    }

    void StartReload()
    {
        PWM.canChange = false;
        reloading = true;
        canFire = false;
        controller.SetBool("Reload", true);
        controller.speed = accuracy == accuracyBase ? 1 : 0.5f;
        if (reload.enabled) reload.enabled = false;
        reload.enabled = true;
    }

    void StopReload()
    {
        PWM.canChange = true;
        spareAmmo += ammo;
        ammo = 0;
        if (spareAmmo < magCapasity)
        {
            ammo = spareAmmo;
            spareAmmo = 0;
        }
        else
        {
            ammo = magCapasity;
            spareAmmo -= magCapasity;
        }
        ammoText.text = ammo.ToString();
        spareAmmoText.text = spareAmmo.ToString();
        reloading = false;
        canFire = true;
        controller.SetBool("Reload", false);
        pController.canMove = true;
        controller.speed = 1;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    IEnumerator DestroyLineRenderer(LineRenderer lr)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(lr.gameObject);
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.enabled = false;
    }
}
