using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 playerPos;
    private Vector3 mousePos;
    public float dex = .05f;
    private float timer;


    [SerializeField] public WeaponBase weapon;

    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;

    List<WeaponBase> weapons;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        timer = dex;
    }
    private void Awake()
    {
        mousePos = Input.mousePosition;
        timer = dex;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        //shooting mechanic
        //If m1 clicked
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            
            Debug.Log("Mouse Button clicked");
            // Get mouse pos
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            // Get player pos
            playerPos = transform.position;
            Vector3 direction = mousePos - transform.position;
            direction.z = 0;
            //get projectile data from weapon
            //shoot projectile
            Debug.Log("playerPos is " + playerPos);
            weapon.Attack(direction, weaponObjectsContainer);
            timer = dex;
        }
        //If mouse 1 held down
        //Shoot
    }

    //private void shootProjectile(projectileBase projectilePrefab, float velocity)
    //{
    //    //Spawn projectile
    //    projectileBase projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    //    Vector3 dir = mousePos - transform.position;
    //    Vector3 rot = transform.position - mousePos;
    //}
}
