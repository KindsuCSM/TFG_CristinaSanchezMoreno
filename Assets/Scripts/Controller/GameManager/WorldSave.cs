using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WorldSave : MonoBehaviour
{
    public static WorldSave Instance { get; private set; }
    private string saveFileName = "savegame.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Funcion para guardar el mundo
    public void SaveGame()
    {
        // Instanciamos la clase saveData que contendrá las clases serializables para que podamos exportarlo a un json
        SaveData saveData = new SaveData();

        // Guardar jugador, el este caso el dinero que es lo unico que nos interesa por el momento
        PlayerController player = GameManager.Instance.playerController;
        saveData.playerData = new PlayerData
        {
            playerMoney = player.GetMoney()
        };

        // Guardar tareas
        saveData.tasks = new List<TaskData>(); // Creamos una lista
        foreach (var task in GameManager.Instance.taskList.getTaskList()) //Recorremos la lista actual que tenemos 
        {
            // Añadimos los objetos de una lista a la que es serializable
            saveData.tasks.Add(new TaskData
            {
                taskName = task.TaskName,
                taskDateEnd = task.DateEnd,
                taskDescription = task.Description,
                taskDatePlanted = task.DatePlanted,
                tileX = task.PlantedTile.x,
                tileY = task.PlantedTile.y,
                tileZ = task.PlantedTile.z,
                plantName = task.PlantsData.PlantName
            });
        }

        // Guardar tiles
        saveData.tiles = new List<TileData>(); // Creamos una lista con objetos serializables
        var tileDictionary = GameManager.Instance.tileManager.getTileDatMap(); // Instanciamos la coleccion que tenemos

        // Como una coleccion no puede ser serializable, separamos los componentes y los intrducimos en la lista, en este caso 
        // guardaremos Vector3Int que seria la key como tres posiciones (x, y, z) independientes
        foreach (var keyValue in tileDictionary)
        {
            TileClass tile = keyValue.Value;
            Vector3Int pos = keyValue.Key;
            saveData.tiles.Add(new TileData
            {
                tileX = pos.x,
                tileY = pos.y,
                tileZ = pos.z,
                isTilled = tile.IsTilled,
                isWatered = tile.IsWatered,
                hasCrop = tile.PlantedPlantCrop != null,
                growthStage = tile.GrowthStage,
                isDead = tile.IsDead,
                plantedPlantName = tile.PlantedPlantCrop != null ? tile.PlantedPlantCrop.PlantData.PlantName : "",
                lastWateredTicks = tile.LastDayWatered.Ticks
            });
        }

        // Guardar inventario
        InventoryController inventory = GameManager.Instance.inventoryPanel.GetComponent<InventoryController>(); // Obtenemos una instancia del inventario
        saveData.inventorySlots = new List<InventorySlotData>(); // Creamos una lista con objetos serializables
        foreach (var slot in inventory.view.itemSlot) // Recorremos con un foreach
        {
            InventorySlotData slotData = new InventorySlotData();
            PlantsData plantData = slot.getPlantData();
            slotData.plantName = plantData != null ? plantData.PlantName : "";
            saveData.inventorySlots.Add(slotData);
        }

        // Serializar a JSON
        string json = JsonUtility.ToJson(saveData, true);

        // Guardar en disco en una ruta específica con el nombre que le hemos dado anteriormente
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);
        Debug.Log($"Guardado en: {path}");
    }

    // funcion para cargar el archivo json y obtener todos los datos
    public void LoadGame()
    {
        ItemList.initList();
        string path = Path.Combine(Application.persistentDataPath, saveFileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning("No hay archivo anterior");
            return;
        }

        // Obtenemos los datos del json
        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        // Cargar player
        PlayerController player = GameManager.Instance.playerController;
        player.SetMoney(saveData.playerData.playerMoney); // Le damos la cifra que hemos obtenido del json

        // Cargar tareas, esta vez pasamos de objetos serializables a los que usamos en partida
        TaskList taskList = GameManager.Instance.taskList;
        foreach (var task in saveData.tasks)
        {
            // Buscar PlantsData por nombre
            PlantsData plant = ItemList.FindPlantDataByName(task.plantName);
            Vector3Int tilePos = new Vector3Int(task.tileX, task.tileY, task.tileZ);
            TaskModel newTask = new TaskModel(task.taskName, task.taskDateEnd, task.taskDescription, task.taskDatePlanted, tilePos, plant);
            taskList.addTask(newTask);
        }

        // Cargar tiles
        var tileManager = GameManager.Instance.tileManager; // Obtenemos una instancia del tileManager
        var tileMap = GameManager.Instance.tileManager.getTileDatMap(); // Obtenemos la coleccion de este

        // Esta vez, al contarrio que antes, convertiremos x, y, z en un Vector3Int y obtendremos una TileClass con la información restante
        foreach (var task in saveData.tiles)
        {
            Vector3Int position = new Vector3Int(task.tileX, task.tileY, task.tileZ);
            TileClass tile = new TileClass
            {
                IsTilled = task.isTilled,
                IsWatered = task.isWatered,
                HasCrop = task.hasCrop,
                GrowthStage = task.growthStage,
                IsDead = task.isDead,
                LastDayWatered = new DateTime(task.lastWateredTicks)
            };
            // Buscar PlantData por nombre
            if (task.plantedPlantName != null)
            {
                PlantsData foundPlant = ItemList.FindPlantDataByName(task.plantedPlantName);
                if (foundPlant != null)
                {
                    tile.PlantedPlantCrop = new PlantedPlantCrop
                    {
                        PlantData = foundPlant
                    };
                }
            }

            if (tileMap.ContainsKey(position))
                tileMap[position] = tile;
            else
                tileMap.Add(position, tile);

            tileManager.paintTiles(position, tile);
        }

        // Cargar inventario
        InventoryController inventory = GameManager.Instance.inventoryPanel.GetComponent<InventoryController>(); // Instancia del inventario

        // Recorremos la lista de saveData
        for (int i = 0; i < saveData.inventorySlots.Count && i < inventory.view.itemSlot.Length; i++)
        {
            var slotData = saveData.inventorySlots[i];
            PlantsData pd = ItemList.FindPlantDataByName(slotData.plantName); //Buscamos la planta en la ItemList mediante el nombre
            if (pd != null)
                inventory.view.itemSlot[i].AddItem(pd); // La añadimos
            else
            {
                inventory.view.itemSlot[i].RemoveItem(); // La "eliminams"
            }
        }

        GameManager.Instance.tileManager.LoadSceneWaterPloughPlant(); // Cargamos los tiles en caso de que esten plantados, regados o harados

        Debug.Log("Juego cargado");
    }
}
