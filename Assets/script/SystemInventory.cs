using UnityEngine;
using System.Collections.Generic;

public class SystemInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public ItemDatabase itemDatabase; 
    private int selectedItemIndex = 0;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) 
    {
        SelectPreviousItem();
    }
    
    if (Input.GetKeyDown(KeyCode.E)) 
    {
        SelectNextItem();
    }

    if (Input.GetKeyDown(KeyCode.F))
    {
        EquipItem(items[selectedItemIndex].itemID);
    }
    
    if (Input.GetKeyDown(KeyCode.U))
    {
        UseEquippedItem();
    }
    }

    public void AddItem(int itemID)
    {
        Item itemToAdd = itemDatabase.GetItemByID(itemID);

        if (itemToAdd != null)
        {
            Item existingItem = items.Find(item => item.itemID == itemToAdd.itemID);
            if (existingItem != null)
            {
                existingItem.AddQuantity(1);
                Debug.Log($"{itemToAdd.itemName} {existingItem.quantity}");
            }
            else
            {
                itemToAdd.AddQuantity(1);
                items.Add(itemToAdd);
                Debug.Log($"{itemToAdd.itemName} {itemToAdd.quantity}");
            }
        }
    }

    public void UseItem(int itemID)
    {
        Item itemToUse = items.Find(item => item.itemID == itemID);
        if (itemToUse != null && itemToUse.quantity > 0)
        {
            Debug.Log($"{itemToUse.itemName} {itemToUse.quantity - 1}");
            itemToUse.AddQuantity(-1); 

            if (itemToUse.quantity <= 0)
            {
                items.Remove(itemToUse);
            }
        }
        else
        {
            Debug.Log("error");
        }
    }

    public void EquipItem(int itemID)
    {
        Item itemToEquip = items.Find(item => item.itemID == itemID);
        
        if (itemToEquip != null && itemToEquip.isUsable && !itemToEquip.isInUse)
        {
            itemToEquip.isInUse = true;
            Debug.Log($"{itemToEquip.itemName} is now equipped.");
        }
        else
        {
            Debug.Log("Item cannot be equipped or is already in use.");
        }
    }

    public void UseEquippedItem()
    {
        Item itemToUse = items.Find(item => item.isInUse == true);
        
        if (itemToUse != null && itemToUse.quantity > 0)
        {
            Debug.Log($"Using {itemToUse.itemName}.");
            itemToUse.AddQuantity(-1); 

            if (itemToUse.quantity <= 0)
            {
                items.Remove(itemToUse);
            }
            itemToUse.isInUse = false;
        }
        else
        {
            Debug.Log("No item to use or item quantity is zero.");
        }
    }

    public void SelectNextItem()
    {
        if (items.Count == 0) return; 
        selectedItemIndex = (selectedItemIndex + 1) % items.Count; 
        Debug.Log($"Selected Item: {items[selectedItemIndex].itemName}");
    }

    public void SelectPreviousItem()
    {
        if (items.Count == 0) return; 
        selectedItemIndex = (selectedItemIndex - 1 + items.Count) % items.Count; 
        Debug.Log($"Selected Item: {items[selectedItemIndex].itemName}");
    }

}
