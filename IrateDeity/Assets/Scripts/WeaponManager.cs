using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Vector3 playerPos;
    private Vector3 mousePos;

    [SerializeField] public WeaponBase weapon;

    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;

    List<WeaponBase> weapons;

    private void Awake()
    {
        mousePos = Input.mousePosition;
    }

    private void Update()
    {
        //shooting mechanic
        //If m1 clicked
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Button clicked");
            // Get mouse pos
            mousePos = Input.mousePosition;
            // Get player pos
            playerPos = transform.position;
            Vector3 direction = mousePos - playerPos;
            //get projectile data from weapon
            //shoot projectile
            Debug.Log("playerPos is " + playerPos);
            weapon.Attack(direction, weaponObjectsContainer);
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
