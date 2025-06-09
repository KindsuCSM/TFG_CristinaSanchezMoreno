using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    public GameObject shopPanel;
    public Button btnCloseWindow;
    public Button btnBuyItem;
    public Image imageItemSelected;
    public TextMeshProUGUI tvPlantName;
    public TextMeshProUGUI tvPlantCost;
    public TextMeshProUGUI tvPlantDescription;
    public Sprite emptySlotImage;
    public ShopSlotController[] itemSlots;

    public void SetSelectedItemVisual(PlantsData plantData)
    {
        if (plantData != null)
        {
            imageItemSelected.sprite = plantData.Icon;
            tvPlantName.text = plantData.PlantName;
            tvPlantCost.text = plantData.Cost.ToString();
            tvPlantDescription.text = plantData.PlantDescription;
        }
        else
        {
            imageItemSelected.sprite = emptySlotImage;
        }
    }

    public void DeselectAll()
    {
        foreach (var slot in itemSlots)
        {
            slot.Deselect();
        }
    }
}
