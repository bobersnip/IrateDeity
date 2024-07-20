using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int experience = 0;
    int level = 1;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> upgradeChoices;
    [SerializeField] List<UpgradeData> acquiredUpgrades;

    WeaponManager weaponManager;

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Start()
    {
        experienceBar.SetLevelText(level);
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if (upgradeChoices == null)
        {
            upgradeChoices = new List<UpgradeData>();
        }
        upgradeChoices.Clear();
        upgradeChoices.AddRange(GetUpgrades(3));

        upgradePanelManager.OpenUpgradePanel(upgradeChoices);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }

    public void Upgrade(int selectedUpgrade)
    {
        UpgradeData upgradeData = upgradeChoices[selectedUpgrade];

        if (acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                //weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.WeaponUnlock:
                //weaponManager.AddWeapon(upgradeData.weaponStats);
                break;
            case UpgradeType.ItemUnlock:
                break;
            default:
                break;
        }

        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }

    internal void AddAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        this.upgrades.AddRange(upgradesToAdd);
    }
}
