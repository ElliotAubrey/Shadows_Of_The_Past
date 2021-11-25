using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _walkSpeed = 3f;
    [SerializeField] float _runSpeed = 5f;
    [SerializeField] float staminaDrain = 0.5f;
    [SerializeField] float staminaRegen = 0.5f;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Slider stamSlider;
    [SerializeField] PlayerGun gun;
    [SerializeField] PlayerLantern lantern;
    [SerializeField] Animator controller;

    public bool canMove = true;
    public bool isMoving;

    float speed, baseWalk, baseRun;
    float stamima = 100f;
    float drainValue;
    float regenValue;

    Vector2 movementDir;
    
    Camera cam;
    Vector2 mousePos;

    private void Awake()
    {
        drainValue = staminaDrain / 50;
        regenValue = staminaRegen / 50;
        baseRun = _runSpeed;
        baseWalk = _walkSpeed;
        gun = FindObjectOfType<PlayerGun>();
        lantern = FindObjectOfType<PlayerLantern>();
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        speed = Input.GetKey(KeyCode.LeftShift) && stamima > 0 && !gun.reloading ? _runSpeed : _walkSpeed;
        movementDir = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        isMoving = body.velocity.magnitude != 0 ? true : false;
    }

    private void FixedUpdate()
    {
        if (canMove) body.velocity = movementDir * speed;
        if (speed > _walkSpeed && body.velocity.magnitude >=1) stamima -= drainValue;
        else if (stamima < 100)
        {
            if(body.velocity.magnitude >= 1) stamima += regenValue/2;
            else stamima += regenValue;
            stamima = Mathf.Clamp(stamima, 0, 100);
        }
        stamSlider.value = stamima;

        Vector2 lookDir = mousePos - body.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        body.rotation = angle;

        if(body.velocity.magnitude != 0) controller.SetBool("IsMoving", true);
        else controller.SetBool("IsMoving", false);
        controller.SetBool("GunEquiped", gun.enabled);
        controller.SetBool("LanternEquiped", lantern.enabled);
        controller.speed = speed == _walkSpeed ? 1 : 2;
    }

    public void MovementDisabled()
    {
        _walkSpeed = _walkSpeed/100 * 50;
        _runSpeed = _runSpeed/100 * 50;
    }

    public void MovementEnabled()
    {
        _walkSpeed = baseWalk;
        _runSpeed = baseRun;
    }

    public void SetVelocity(Vector2 velocity)
    {
        body.velocity = velocity;
    }

    public void SetStamina(float amount)
    {
        stamima = amount;
    }
}
