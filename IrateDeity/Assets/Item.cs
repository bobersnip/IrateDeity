using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private string itemName;

    [SerializeField] private int quantity;

    [SerializeField] private Sprite sprite;

    private InventoryManager inventoryManager;
    public ItemType itemType;


    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryPanel").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(itemName, quantity, sprite, itemType);
            Destroy(gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        inventoryManager.AddItem(itemName, quantity, sprite, itemType);
    //        Destroy(gameObject);
    //    }
    //}

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

}
