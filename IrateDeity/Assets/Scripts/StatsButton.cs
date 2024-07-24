using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsButton : MonoBehaviour
{

    [SerializeField] GameObject InventoryScreen;
    [SerializeField] GameObject StatsScreen;

    private void Awake()
    {
        StatsScreen.SetActive(false);
    }
    public void DisplayStatsScreen()
    {
        InventoryScreen.SetActive(false);
        StatsScreen.SetActive(true);
    }
}
