using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaggerWeapon : WeaponBase
{
    private void Awake()
    {
    }

    private void SpawnDagger(Vector3 direction, Transform wepObj)
    {
        //Vector3 direction = new Vector3(playerMove.lastHorizontalVector, 0f).normalized;

        // maxangle = 20, startingDegrees = 10
        Debug.Log("IN SPAWN DAGGER " + direction.normalized);
        float startingDegrees = weaponStats.maxAngle / 2;
        if (weaponStats.numberOfAttacks == 1)
        {
            startingDegrees = 0f;
        }

        float currentDegrees = -startingDegrees;
        float spread = weaponStats.maxAngle / (weaponStats.numberOfAttacks - 1);

        // Adjust to set the starting direction
        //Quaternion startingRotation = Quaternion.AngleAxis((startingDegrees), Vector3.back);
        //direction = startingRotation * direction;
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Debug.Log("transform is " + transform);
            GameObject thrownDagger = Instantiate(projectilePrefab, wepObj);

            thrownDagger.transform.position = wepObj.position;
            ThrowingDaggerProjectile throwingDaggerProjectile = thrownDagger.GetComponent<ThrowingDaggerProjectile>();
                // throwing angle changes based on even or odd # of proj
            throwingDaggerProjectile.SetDirection(
                direction.normalized,
                currentDegrees,
                direction.x
            );
            throwingDaggerProjectile.damage = weaponStats.damage;
            

            // Adjust angle to new angle
            direction = Quaternion.AngleAxis(spread, Vector3.forward) * direction;
            currentDegrees += spread;
        }
    }

    public override void Attack(Vector3 direction, Transform wepObj)
    {
        Debug.Log("ATTACK IS CALLED");
        SpawnDagger(direction, wepObj);
    }
}