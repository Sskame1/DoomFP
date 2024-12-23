using UnityEngine;

public enum ItemType
{
    Consumable, // Используемые на игроке (например, аптечки)
    Equipable,  // Одеваемые предметы (например, броня)
    Resource     // Ресурсы (например, патроны)
}

public abstract class Item : MonoBehaviour // Наследуем от MonoBehaviour для использования в Unity
{
    public string itemName; // Название предмета
    public ItemType itemType; // Тип предмета
    public int count; // Количество предметов

    // Метод для использования предмета
    public virtual void Use()
    {
        if (count > 0)
        {
            Debug.Log($"Used item: {itemName}");
            count--;
            // Логика использования предмета (вызывается в производных классах)
        }
        else
        {
            Debug.Log("No more items to use!");
        }
    }
}
