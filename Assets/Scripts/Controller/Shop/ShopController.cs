using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    // Variables 
    [SerializeField] private ShopView view;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private PlayerController playerController;

    private ShopModel model;
    private PlantsData selectedPlant;
    private ShopSlotController selectedSlot;

    // 
    void Start()
    {
        model = new ShopModel(); // Instancia un model
        model.LoadItems(ItemList.getList()); // Cargamos la lista de items

        inventory = GameManager.Instance.inventoryPanel.GetComponent<InventoryController>(); // Obtenemos el inventario
        playerController = PlayerController.Instance.GetComponent<PlayerController>(); // Obtenemos al jugador 

        // Creamos los listeners de los btn
        view.btnCloseWindow.onClick.AddListener(() => view.shopPanel.SetActive(false));
        view.btnBuyItem.onClick.AddListener(BuyItem);

        // Rellenamos la tienda con los elementos que hemos obtenido
        for (int i = 0; i < model.availableItems.Count && i < view.itemSlots.Length; i++)
        {
            view.itemSlots[i].SetItem(model.availableItems[i], 1);
        }
    }

    public void OnItemSelected(PlantsData plant, ShopSlotController slot)
    {
        view.DeselectAll(); // Deseleccionamos los items de la tienda
        selectedPlant = plant; // Guardamos la planta seleccionada
        selectedSlot = slot; // guardamos el slot de la tienda donde está

        view.SetSelectedItemVisual(plant); // Mostramos visualmente la planta
    }

    private void BuyItem()
    {
        if (selectedPlant == null || !model.CanAfford(playerController, selectedPlant)) return;

        model.PurchaseItem(playerController, selectedPlant); // Llamamos a la funcion para comprarlo
        inventory.AddItem(selectedPlant, 1); // Añadimos al inventario
        selectedSlot.ClearItem(); // Eliminamos de la tienda

        selectedPlant = null; // Seteamos la planta seleccionada a null
        selectedSlot = null; // Seteamos el slot a null

        view.DeselectAll(); // Deseleccionamos todas 
        view.SetSelectedItemVisual(null); // Eliminamos la planta de forma visual
    }
}
