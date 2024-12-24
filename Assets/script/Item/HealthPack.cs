using UnityEngine;

public class HealthPack : ItemBase
{
    public int healthRestored; // Количество восстановленного здоровья

    // Метод для использования аптечки
    public override void Use()
    {
        base.Use(); // Вызов базового метода
        // Логика восстановления здоровья
        Debug.Log($"Restored {healthRestored} health.");
        // Ваш код для восстановления здоровья игроку
    }
}
