using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaggerWeapon : WeaponBase
{
    //private void Start()
    //{
    //    Debug.Log("Weapon Data Is Set");
    //    SetData(weaponData);
    //}

    private void SpawnDagger(Vector3 direction, Vector3 spawnPoint, float projRotation)
    {
        //Vector3 direction = new Vector3(playerMove.lastHorizontalVector, 0f).normalized;

        //maxangle = 20, startingDegrees = 10
        Debug.Log("IN SPAWN DAGGER " + direction.normalized);
        float startingDegrees = weaponStats.maxAngle / 2;
        if (weaponStats.numberOfAttacks == 1)
        {
            startingDegrees = 0f;
        }

        float currentDegrees = -startingDegrees;
        float spread = weaponStats.maxAngle / (weaponStats.numberOfAttacks - 1);

        //Adjust to set the starting direction
        Quaternion startingRotation = Quaternion.AngleAxis((startingDegrees), Vector3.back);
        direction = startingRotation * direction;

        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Debug.Log("Spawning On " + spawnPoint);
            Debug.Log("Rotation is" + projRotation);

            GameObject thrownDagger = Instantiate(projectilePrefab, spawnPoint, Quaternion.Euler(0,0, -projRotation));
            ThrowingDaggerProjectile throwingDaggerProjectile = thrownDagger.GetComponent<ThrowingDaggerProjectile>();
            throwingDaggerProjectile.transform.localRotation *= Quaternion.AngleAxis(currentDegrees, Vector3.forward);
            throwingDaggerProjectile.direction = direction;

                // throwing angle changes based on even or odd # of proj
           /*throwingDaggerProjectile.SetDirection(
                direction.normalized,
                currentDegrees,
                direction.x
            );*/
            throwingDaggerProjectile.damage = weaponStats.damage;
            

            // Adjust angle to new angle
            direction = Quaternion.AngleAxis(spread, Vector3.forward) * direction;
            currentDegrees += spread;
        }
    }

    public override void Attack(Vector3 direction, Vector3 spawnPoint, float projRotation)
    {
        Debug.Log("ATTACK IS CALLED");
        SpawnDagger(direction, spawnPoint, projRotation);
    }
}