using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public ItemType itemType;
    public Item item;

    // ITEM SLOT
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private EquippedSlot weaponSlot, abilitySlot, armorSlot, ringSlot;
    // public bool thisItemSelected;

    public void AddItem(string itemName, int quantity, Sprite sprite, ItemType itemType)
    {
        if (!isFull)
        {
            this.itemType = itemType;
            this.itemName = itemName;
            this.quantity = quantity;
            this.itemSprite = sprite;

            isFull = true;
            itemImage.sprite = itemSprite;
        }
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && eventData.clickCount == 2)
        {
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
        switch (item.itemType)
        {
            case ItemType.weapon:
                Debug.Log("equipping weapon");
                break;
            case ItemType.ability:
                Debug.Log("equipping ability");
                break;
            case ItemType.ring:
                Debug.Log("equipping ability");
                break;
            case ItemType.armor:
                Debug.Log("equipping armor");
                break;

            default:
                Debug.Log("Don't do anything");
                break;
        }

    }

    public void DropItem()
    {

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
