using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    public List<PlantsData> availableItems = new();
    
    public void LoadItems(List<GameObject> gameObjects)
    {
        availableItems.Clear();

        foreach (var obj in gameObjects)
        {
            var item = obj.GetComponent<ItemController>();
            if (item != null && item.plantsData != null)
            {
                availableItems.Add(item.plantsData);
            }
        }
    }

    public bool CanAfford(PlayerController player, PlantsData item)
    {
        return player.GetMoney() >= item.Cost;
    }

    public void PurchaseItem(PlayerController player, PlantsData item)
    {
        int totalMoneyPlayer = player.GetMoney() - item.Cost;
        player.SetMoney(totalMoneyPlayer);
    }
}

