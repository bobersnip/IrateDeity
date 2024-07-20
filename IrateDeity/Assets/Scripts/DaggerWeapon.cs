using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaggerWeapon : WeaponBase
{
    private PlayerMove playerMove;

    [SerializeField] private GameObject daggerPrefab;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    private void SpawnDagger()
    {
        Vector3 direction = new Vector3(playerMove.lastHorizontalVector, 0f).normalized;

        // maxangle = 20, startingDegrees = 10
        float startingDegrees = weaponStats.maxAngle / 2;
        if (weaponStats.numberOfAttacks == 1)
        {
            startingDegrees = 0f;
        }

        float currentDegrees = -startingDegrees;
        float spread = weaponStats.maxAngle / (weaponStats.numberOfAttacks - 1);

        // Adjust to set the starting direction
        Quaternion startingRotation = Quaternion.AngleAxis((startingDegrees), Vector3.back);
        direction = startingRotation * direction;

        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject thrownDagger = Instantiate(daggerPrefab, transform);

            thrownDagger.transform.position = transform.position;
            ThrowingDaggerProjectile throwingDaggerProjectile = thrownDagger.GetComponent<ThrowingDaggerProjectile>();
            if (playerMove != null)
            {
                // throwing angle changes based on even or odd # of proj
                throwingDaggerProjectile.SetDirection(
                    direction,
                    currentDegrees,
                    playerMove.lastHorizontalVector
                );
                throwingDaggerProjectile.damage = weaponStats.damage;
            }

            // Adjust angle to new angle
            direction = Quaternion.AngleAxis(spread, Vector3.forward) * direction;
            currentDegrees += spread;
        }
    }

    public override void Attack()
    {
        SpawnDagger();
    }
}