using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotController : MonoBehaviour, ISlotController, IPointerClickHandler
{
    // Variables
    public InventorySlotView view;
    public InventorySlotModel model;

    private InventoryController inventory;
    private TileManager tileManager;

    // Obtenemos view e inicializamos el model
    private void Awake()
    {
        view = GetComponent<InventorySlotView>();
        model = new InventorySlotModel();
    }

    // Obtenemos el inventoryController y TileManager a partir de sus nombres en escena
    private void Start()
    {
        inventory = GameObject.Find("InventoryPanel").GetComponent<InventoryController>();
        tileManager = GameObject.Find("TileManager").GetComponent<TileManager>();
    }

    // Funcion para añadir una planta al propio hueco
    public void AddItem(PlantsData plantData, int quantity = 1)
    {
        model.SetItem(plantData, quantity); // le damos el item al model
        view.SetSlotVisual(plantData, quantity); // Mostramos la imagen en el slot
    }

    // Funcion que elimina el item del slot
    public void RemoveItem()
    {
        model.ClearItem(); // Elimina el item del model
        view.ClearVisual(); // Elimina el item de lo visual
    }

    //Le damos funcion en caso de que se presione
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    // Funcion que ocurrirá en caso de presionar uno de los huecos
    public void OnClick()
    {
        inventory.view.ClearSelection(); // Deseleccionamos los demas huecos
        view.SetSelected(true);          // Seteamos el boolean a true ya que ha sido seleccionado
        inventory.SelectItem(model.plantData); //Mostramos visualmente la planta seleccionada
        tileManager.setSelectedPlant(model.plantData); // Seteamos que el item que está en este slot es el elegido por el usuario
    }

    // Obtenemos la planta seleccionada
    public PlantsData getPlantData()
    {
        return model.plantData;
    }

    // Seteamos el boolean de haberlo seleccionado
    public void SetSelected(bool selected)
    {
        view.SetSelected(selected);
    }

    // Creamos un getter
    public bool IsFull => model.IsFull;
}
