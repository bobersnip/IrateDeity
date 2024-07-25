using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;
    public WeaponStats weaponStats;
    [SerializeField] public GameObject projectilePrefab;

    public float timeToAttack = 1f;
    private float timer;

    // abstract requires that each weapon will have its own attack method
    // Attack function will spawn a projectile
    public abstract void Attack(Vector3 direction, Vector3 spawnPoint, float projRotation);

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        timeToAttack = weaponData.stats.timeToAttack;

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.numberOfAttacks, wd.stats.maxAngle);
    }

    public virtual void PostMessage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }
}