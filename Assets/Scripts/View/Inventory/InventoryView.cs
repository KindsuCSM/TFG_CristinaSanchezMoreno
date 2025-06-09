using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public RectTransform inventoryMenu;
    public Image imageItemSelectedGlobal;
    public Image imageItemSelectedInventory;
    public InventorySlotController[] itemSlot;
    public TextMeshProUGUI tvPlantName;
    public TextMeshProUGUI tvPlantCost;
    public TextMeshProUGUI tvPlantDescription;

    public void ShowInventory(bool isActive)
    {
        Vector2 windowPosition = inventoryMenu.anchoredPosition;
        Time.timeScale = isActive ? 0 : 1;
        windowPosition.x = isActive ? 0f : -730.93f;
        inventoryMenu.anchoredPosition = windowPosition;
    }

    public void ClearSelection()
    {
        foreach (var slot in itemSlot)
        {
            slot.SetSelected(false);
        }
    }

    public void ShowSelectedItem(PlantsData plantData)
    {
        if (plantData != null)
        {
            imageItemSelectedGlobal.sprite = plantData.Icon;
            imageItemSelectedInventory.sprite = plantData.Icon;
            tvPlantName.text = plantData.PlantName;
            tvPlantCost.text = plantData.Cost.ToString();
            tvPlantDescription.text = plantData.PlantDescription;
        }
        else
        {
            ClearSelection();
        }
    }

    public void AddItemToSlot(PlantsData plantData, int quantity)
    {
        foreach (var slot in itemSlot)
        {
            if (!slot.IsFull)
            {
                slot.AddItem(plantData, quantity);
                return;
            }
        }
    }
}
