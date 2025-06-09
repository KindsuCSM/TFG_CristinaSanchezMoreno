public class InventorySlotModel
{
    public PlantsData plantData;
    public int quantity;

    public bool IsFull => plantData != null;

    public void SetItem(PlantsData plant, int qty)
    {
        plantData = plant;
        quantity = qty;
    }

    public void ClearItem()
    {
        plantData = null;
        quantity = 0;
    }
}
