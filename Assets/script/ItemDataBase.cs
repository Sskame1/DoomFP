using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public Item[] items;

    public Item GetItemByID(int id)
    {
        foreach (Item item in items)
        {
            if (item.itemID == id)
            {
                return item;
            }
        }
        return null;
    }
}