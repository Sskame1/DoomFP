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
            // ���������, ���� �� ��� ������� � ���������
            Item existingItem = items.Find(item => item.itemID == itemToAdd.itemID);
            if (existingItem != null)
            {
                // ����������� ����������
                existingItem.AddQuantity(1);
                Debug.Log($"��������� 1 {itemToAdd.itemName}. ������ �����: {existingItem.quantity}");
            }
            else
            {
                // ��������� ����� ������� � ����������� 1
                itemToAdd.AddQuantity(1);
                items.Add(itemToAdd);
                Debug.Log($"�������� �������: {itemToAdd.itemName}. ����������: {itemToAdd.quantity}");
            }
        }
    }

    public void UseItem(int itemID)
    {
        Item itemToUse = items.Find(item => item.itemID == itemID);
        if (itemToUse != null && itemToUse.quantity > 0)
        {
            // ������ ������������� ��������
            Debug.Log($"����������� {itemToUse.itemName}. ���������� ����������: {itemToUse.quantity - 1}");
            itemToUse.AddQuantity(-1); // ��������� ����������

            // ������� �������, ���� ���������� 0
            if (itemToUse.quantity <= 0)
            {
                items.Remove(itemToUse);
            }
        }
        else
        {
            Debug.Log("���� ������� ����������� � ���������.");
        }
    }
}
