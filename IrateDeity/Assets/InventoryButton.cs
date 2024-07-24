using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{

    [SerializeField] GameObject InventoryScreen;
    [SerializeField] GameObject StatsScreen;

    public void DisplayInventoryScreen()
    {
        StatsScreen.SetActive(false);

        InventoryScreen.SetActive(true);
    }
}
