using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] WeaponData startingWeapon;

    List<WeaponBase> weapons;

    private void Awake()
    {
        weapons = new List<WeaponBase>();
    }

    private void Start()
    {
        AddWeapon(startingWeapon);
    }

    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);
        WeaponBase weapondBase = weaponGameObject.GetComponent<WeaponBase>();

        weapondBase.SetData(weaponData);
        weapons.Add(weapondBase);

        Level level = GetComponent<Level>();
        if (level != null)
        {
            level.AddAvailableUpgrades(weaponData.weaponUpgrades);
        }
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponStats);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
