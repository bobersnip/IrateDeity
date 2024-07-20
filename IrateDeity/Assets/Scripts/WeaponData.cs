using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;
    public float maxAngle;

    public WeaponStats(int damage, float timeToAttack, int numberOfAttacks, float maxAngle)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.numberOfAttacks = numberOfAttacks;
        this.maxAngle = maxAngle;
    }

    public void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack *= weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        if (this.maxAngle + weaponUpgradeStats.maxAngle < 0)
        {
            this.maxAngle = 0;
        }
        else
        {
            this.maxAngle += weaponUpgradeStats.maxAngle;
        }
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    //public List<UpgradeData> weaponUpgrades;
}