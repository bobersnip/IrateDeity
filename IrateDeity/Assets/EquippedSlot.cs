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

    // ITEM SLOT
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private EquippedSlot weaponSlot, abilitySlot, armorSlot, ringSlot;
    // public bool thisItemSelected;

    // For Item Moving
    private InventoryManager inventoryManager;

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
        inventoryManager.AddItem(itemName, quantity, itemSprite, itemType);
        RemoveCurrentItem();

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

    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
