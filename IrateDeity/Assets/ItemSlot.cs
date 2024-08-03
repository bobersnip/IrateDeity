using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;
using System.Diagnostics.Tracing;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public ItemType itemType;
    // public Item item;

    // ITEM SLOT
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private EquippedSlot weaponSlot, abilitySlot, armorSlot, ringSlot;
    // public bool thisItemSelected;

    public void AddItem(string itemName, int quantity, Sprite sprite, ItemType itemType)
    {
        this.itemType = itemType;
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = sprite;

        isFull = true;
        itemImage.sprite = itemSprite;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2) {
            UseItem();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
    }
    public void UseItem()
    {
        // thisItemSelected = true;

        // If this can be moved into an equipment slot, do it.
        switch (itemType)
        {
            case ItemType.weapon:
                Debug.Log("equipping weapon");
                weaponSlot.AddItem(itemName, quantity, itemSprite, itemType);
                RemoveCurrentItem();
                break;
            case ItemType.ability:
                Debug.Log("equipping ability");
                abilitySlot.AddItem(itemName, quantity, itemSprite, itemType);
                RemoveCurrentItem();
                break;
            case ItemType.ring:
                Debug.Log("equipping ability");
                ringSlot.AddItem(itemName, quantity, itemSprite, itemType);
                RemoveCurrentItem();
                break;
            case ItemType.armor:
                Debug.Log("equipping armor");
                armorSlot.AddItem(itemName, quantity, itemSprite, itemType);
                RemoveCurrentItem();
                break;

            default:
                Debug.Log("Don't do anything");
                break;
        }

    }

    private void RemoveCurrentItem()
    {
        itemName = "";
        quantity = 0;
        isFull = false;
        itemSprite = null;
        itemImage.sprite = null;
    }

    public void DropItem()
    {
        // TODO: Make item drop on ground

        RemoveCurrentItem();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
