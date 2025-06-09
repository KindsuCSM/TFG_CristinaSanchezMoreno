using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    public TMP_Text quantityText;
    public Image itemImage;
    public GameObject selectedItem;

    private Sprite emptySlotImage;

    private void Awake()
    {
        emptySlotImage = Resources.Load<Sprite>("Images/UI/EmptyInventorySlot");
    }

    public void SetSlotVisual(PlantsData plantData, int quantity)
    {
        if (plantData != null)
        {
            itemImage.sprite = plantData.Icon;
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
        }
        else
        {
            ClearVisual();
        }
    }

    public void ClearVisual()
    {
        itemImage.sprite = emptySlotImage;
        quantityText.text = "";
        quantityText.enabled = false;
        selectedItem.SetActive(false);
    }

    public void SetSelected(bool selected)
    {
        selectedItem.SetActive(selected);
    }
}
