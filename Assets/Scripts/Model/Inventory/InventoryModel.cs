using System.Collections.Generic;

public class InventoryModel
{
    public List<InventoryItemData> items = new();
    public PlantsData selectedPlant;

    public void AddItem(PlantsData plantData, int quantity)
    {
        items.Add(new InventoryItemData(plantData, quantity));
    }

    public class InventoryItemData
    {
        public PlantsData plantData;
        public int quantity;

        public InventoryItemData(PlantsData plantData, int quantity)
        {
            this.plantData = plantData;
            this.quantity = quantity;
        }
    }
}
