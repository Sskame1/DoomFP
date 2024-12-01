using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemID;
    public int quantity;
    public bool isUsable;
    public bool isInUse;

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }

    public void ResetQuantity()
    {
        quantity = 0;
    }
}