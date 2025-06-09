using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //Variables
    public Button btnManageOpenClose;
    public InventoryView view;
    private InventoryModel model;
    private bool isInventoryActive = false;

    // Damos funcion al btn e inicializamos model
    void Start()
    {
        model = new InventoryModel();

        btnManageOpenClose.onClick.AddListener(() =>
        {
            isInventoryActive = !isInventoryActive;
            view.ShowInventory(isInventoryActive);
            view.ClearSelection();
        });
    }

    // Funcion para añadir un item al inventario
    public void AddItem(PlantsData plantData, int quantity)
    {
        model.AddItem(plantData, quantity);
        view.AddItemToSlot(plantData, quantity);
    }

    // Funcnion que setea la planta seleccionada en el model y la muestra en su respectivo recuadro
    public void SelectItem(PlantsData plantData)
    {
        model.selectedPlant = plantData;
        view.ShowSelectedItem(plantData);
    }

    // Funcion para obtener la planta seleccionada
    public PlantsData GetSelectedPlantData()
    {
        return model.selectedPlant;
    }
}
