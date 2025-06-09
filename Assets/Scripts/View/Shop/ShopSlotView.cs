using UnityEngine;
using UnityEngine.UI;

public class ShopSlotView : MonoBehaviour
{
    public Image itemImage;
    public GameObject selectedItem;
    private Sprite emptySlotImage;

    void Awake()
    {
        emptySlotImage = Resources.Load<Sprite>("Images/UI/EmptyInventorySlot");
    }

    public void SetImage(Sprite sprite)
    {
        itemImage.sprite = sprite != null ? sprite : emptySlotImage;
    }

    public void SetSelected(bool selected)
    {
        selectedItem.SetActive(selected);
    }
}
