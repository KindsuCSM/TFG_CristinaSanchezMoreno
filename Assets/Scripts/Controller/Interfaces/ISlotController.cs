using UnityEngine.EventSystems;

public interface ISlotController : IPointerClickHandler
{
    // Interfaz creada ya que InventorySlotController y ShopSclotsController llevan funciones muy parecidas
    void AddItem(PlantsData plantData, int quantity);
    new void OnPointerClick(PointerEventData eventData);
    void OnClick();
}
