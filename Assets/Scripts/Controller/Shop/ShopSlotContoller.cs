using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ShopSlotView))]
public class ShopSlotController : MonoBehaviour, IPointerClickHandler
{
    // Variables
    private SlotModel model = new();
    private ShopController shopController;
    public ShopSlotView view;

    // Obtenemos los componentes view y shopController
    void Start()
    {
        view = GetComponent<ShopSlotView>();
        shopController = FindObjectOfType<ShopController>();
    }

    // Seteamos la planta al model y mostramos visualmente
    public void SetItem(PlantsData plantData, int quantity)
    {
        model.plantData = plantData;
        view.SetImage(plantData?.Icon);
    }

    // Eliminamos la planta seleccionada
    public void ClearItem()
    {
        model.plantData = null;
        view.SetImage(null);
        view.SetSelected(false);
    }
    
    // Deseleccionamos el slot
    public void Deselect()
    {
        model.isSelected = false;
        view.SetSelected(false);
    }

    // Si clickamos sobre el slot lo seleccionaremos
    public void OnPointerClick(PointerEventData eventData)
    {
        if (model.plantData == null) return;

        shopController.OnItemSelected(model.plantData, this);
        model.isSelected = true;
        view.SetSelected(true);
    }

    // Getter para obtener la planta
    public PlantsData GetPlantData() => model.plantData;
}
