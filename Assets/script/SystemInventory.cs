using UnityEngine;
using System.Collections.Generic;

public class SystemInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public ItemDatabase itemDatabase; 


    void Start()
    {
        
    }
    public void AddItem(int itemID)
    {
        Item itemToAdd = itemDatabase.GetItemByID(itemID);

        if (itemToAdd != null)
        {
            // Проверяем, есть ли уже предмет в инвентаре
            Item existingItem = items.Find(item => item.itemID == itemToAdd.itemID);
            if (existingItem != null)
            {
                // Увеличиваем количество
                existingItem.AddQuantity(1);
                Debug.Log($"Добавлено 1 {itemToAdd.itemName}. Теперь всего: {existingItem.quantity}");
            }
            else
            {
                // Добавляем новый предмет с количеством 1
                itemToAdd.AddQuantity(1);
                items.Add(itemToAdd);
                Debug.Log($"Подобран предмет: {itemToAdd.itemName}. Количество: {itemToAdd.quantity}");
            }
        }
    }

    public void UseItem(int itemID)
    {
        Item itemToUse = items.Find(item => item.itemID == itemID);
        if (itemToUse != null && itemToUse.quantity > 0)
        {
            // Логика использования предмета
            Debug.Log($"Использован {itemToUse.itemName}. Оставшееся количество: {itemToUse.quantity - 1}");
            itemToUse.AddQuantity(-1); // Уменьшаем количество

            // Удаляем элемент, если количество 0
            if (itemToUse.quantity <= 0)
            {
                items.Remove(itemToUse);
            }
        }
        else
        {
            Debug.Log("Этот предмет отсутствует в инвентаре.");
        }
    }
}
