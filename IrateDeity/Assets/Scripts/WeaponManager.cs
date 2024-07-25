using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 playerPos;
    private Vector3 mousePos;
    public float dex = .02f;
    private float timer;

    [SerializeField] public WeaponBase weapon;

    //[SerializeField] Transform weaponObjectsContainer;
    //[SerializeField] WeaponData startingWeapon;

    List<WeaponBase> weapons;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        timer = dex;
    }
    private void Awake()
    {
        weapon.SetData(weapon.weaponData);
        mousePos = Input.mousePosition;
        timer = dex;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        //shooting mechanic
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            // Get mouse pos
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            // Get player pos
            playerPos = transform.position;
            //Calculate direction vector and projectile rotation
            Vector3 direction = mousePos - transform.position;
            direction.z = 0;
            float rotZ = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            //Attack RAHHH
            weapon.Attack(direction, playerPos, rotZ);
            //Reset timer
            timer = dex;
        }
    }
}
