using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemList
{
    private static List<GameObject> lstItems = new List<GameObject>();
    private static bool isInitialized = false;

    public static void initList()
    {
        if (isInitialized) return;

        string[] prefabNames = { "Azure Cornflower", "Blue Borage", "Butterfly Lavender", "Cecilia Flower", "Dendrobium", "QingXin" };

        foreach (string name in prefabNames)
        {
            GameObject prefab = Resources.Load<GameObject>("PrefabsPlantas/" + name);
            if (prefab != null)
            {
                lstItems.Add(prefab);
                Debug.Log("Cargado prefab: " + name);
            }
            else
            {
                Debug.LogWarning("No se pudo cargar o falta PlantsData: " + name);
            }
        }

        Debug.Log("Total prefabs cargados: " + lstItems.Count);
        
        isInitialized = true;
    }

    public static List<GameObject> getList()
    {
        if (!isInitialized)
            initList();

        return lstItems;
    }
    public static PlantsData FindPlantDataByName(string name)
    {
        foreach (var prefab in lstItems)
        {
            var item = prefab.GetComponent<ItemController>();
            if (item != null && item.plantsData != null && item.plantsData.PlantName == name)
            {
                return item.plantsData;
            }
        }
        return null;
    }

}
