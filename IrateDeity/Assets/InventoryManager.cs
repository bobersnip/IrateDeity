using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(string itemName, int quantity, Sprite sprite, ItemType itemType)
    {
        // Debug.Log("itemname: " + itemName + "\nquantity: " + quantity);
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, sprite, itemType);
                return;
            }
        }
    }
}

public enum ItemType
{
    consumable,
    weapon,
    ability,
    armor,
    ring
}; 