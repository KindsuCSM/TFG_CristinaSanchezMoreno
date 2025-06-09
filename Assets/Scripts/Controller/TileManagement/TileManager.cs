using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    //Variables publicas a la que le asignaremos en escena sus respectivos Map
    public Tilemap ploughMap, waterMap, plantMap;
    // Variable publica a la que le asignaremos en escena los TileRules correspondientes
    public RuleTile ploughRule, waterRule;
    // Variable privada donde guardaremos la informacion de la tile sobre la que estamos ejecutando las acciones
    private Dictionary<Vector3Int, TileClass> tileDatMap = new Dictionary<Vector3Int, TileClass>();
    private DateTime currentDay;
    public int daysPassed;
    public PlantsData selectedPlant;
    public TaskPanelController taskPanel;
    public InventoryController inventory;
    public bool simulation = false;
    public int simulatedDays = 0;

    void Awake()
    {
        // Hace que el GameObject se destruya al cargar otra escena
        DontDestroyOnLoad(gameObject);
        // Cada vez que se cargue una escena se ejecuta OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Cuando se llame al metodo OnSceneLoaded llamaremos a setTileMaps que cargará los Maps de escena 
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("Escena cargada: " + scene.name);
        setTileMaps();
        if (scene.name == "HomeScene")
        {
            LoadSceneWaterPloughPlant();
        }
    }

    // Dibujamos las tiles y obtenemos el taskpanel e inventory
    void Start()
    {
        setTileMaps();
        currentDay = DateTime.Today;
        taskPanel = GameObject.Find("CreateTaskPanel").GetComponent<TaskPanelController>();
        inventory = GameObject.Find("InventoryPanel").GetComponent<InventoryController>();
    }

    void Update()
    {
        // Simulamos el crecimiento
        if (simulation)
        {
            calculateGrowthDay();
            simulation = false;
        }

        // Si cambia de dia, actualizamos las tiles
        if (DateTime.Today > currentDay)
        {
            currentDay = DateTime.Today;
            NewDayUpdateWaterPlantPlough();
        }
    }

    // Restauramos el estado de las tiles al volver de otra escena
    public void LoadSceneWaterPloughPlant()
    {
        foreach (TaskModel task in GameManager.Instance.taskList.getTaskList())
        {
            Vector3Int tile = task.PlantedTile;
            PlantsData plant = task.PlantsData;

            PlantFromTask(tile, plant, task);
            PloughWaterFromDictionary(tileDatMap);
        }
    }

    // Obtenemos los tilemap creados para los tiles de regar, plantar y harar
    private void setTileMaps()
    {
        try
        {
            // Obtenemos el GameObject que contiene los TileMaps
            GameObject sceneLayers = GameObject.Find("SceneLayers");
            // Si no es null se cargarán los maps dependiendo del nombre y se lo asignaremos a su respectiva variable
            if (sceneLayers != null)
            {
                // Recogemos en un Array los componentes del SceneLayers que hemos recogido anteriormente
                Tilemap[] tilemaps = sceneLayers.GetComponentsInChildren<Tilemap>();
                // Bucle que comprueba si hay alguno de los componentes que se llamen Plough, Water y Plant y los asigna si es asi
                foreach (Tilemap tilemap in tilemaps)
                {
                    if (tilemap.name == "Plough")
                    {
                        ploughMap = tilemap;
                    }
                    else if (tilemap.name == "Water")
                    {
                        waterMap = tilemap;
                    }
                    else if (tilemap.name == "Plant")
                    {
                        plantMap = tilemap;
                    }
                }
            }
        }
        catch (Exception e)
        {
            ploughMap = null;
            waterMap = null;
            plantMap = null;
            Debug.Log(e.Message);
        }
    }
    // Funcion para ejecutar la accion del personaje sobre una celda del mapa
    public void PerformTileAction(string action, Vector3Int cell)
    {
        // Si la celda no existe en nuestra colección, se agrega
        if (!tileDatMap.ContainsKey(cell))
        {
            tileDatMap[cell] = new TileClass();
        }
        // Obtenemos la instancia de los datos de esta celda para trabajar sobre ella
        TileClass tile = tileDatMap[cell];
        //Comprobamos si los mapas no son null
        if (ploughMap != null && ploughRule != null && plantMap != null)
        {
            switch (action)
            {
                case "water":
                    if (!tile.IsWatered)
                    {
                        tile.IsWatered = true;
                        tile.LastDayWatered = DateTime.Today;
                        waterMap.SetTile(cell, waterRule);
                        Debug.Log("Regando en " + cell);
                    }
                    break;
                case "plough":
                    if (!tile.IsTilled)
                    {
                        tile.IsTilled = true;
                        ploughMap.SetTile(cell, ploughRule);
                        Debug.Log("Harando en " + cell);
                    }
                    break;
                case "harvest": break;
                case "plant":
                    if (selectedPlant != null && tile.IsTilled == true)
                    {
                        Debug.Log("Celda a plantar:" + cell);
                        taskPanel.OpenWindow(cell);
                    }
                    break;
            }
        }
    }

    // Funcion para obtener los datos de la celda que le pasemos como parámetro
    public TileClass getTilesDat(Vector3Int cell)
    {
        if (tileDatMap.TryGetValue(cell, out TileClass tile))
        {
            return tile;
        }
        return null;
    }

    // Funcion para dibujar la planta en escena
    public void plantAction(TileClass tile, Vector3Int cell, int definedGrowthDays)
    {
        if (tile.IsTilled && tile.PlantedPlantCrop == null)
        {
            if (selectedPlant != null)
            {
                tile.PlantedPlantCrop = new PlantedPlantCrop
                {
                    PlantData = selectedPlant,
                    DayPlanted = DateTime.Today,
                    DefinedGrowthDays = definedGrowthDays,
                    CurrentGrowthState = 0
                };

                paintPlantOnMap(cell, selectedPlant);
            }
        }
    }
    
    // Funcion para pintar cuando hayamos creado una tarea
    private void PlantFromTask(Vector3Int cell, PlantsData plantData, TaskModel task)
    {
        // Si no existe esa celda, la creamos
        if (!tileDatMap.ContainsKey(cell))
        {
            tileDatMap[cell] = new TileClass();
        }

        TileClass tileClass = tileDatMap[cell];

        tileClass.IsTilled = true;

        // Simula que se ha plantado directamente (sin abrir ventana de tarea)
        tileClass.PlantedPlantCrop = new PlantedPlantCrop
        {
            PlantData = plantData,
            DayPlanted = task.DatePlanted,
            DefinedGrowthDays = task.DateEnd,
            CurrentGrowthState = 0
        };

        paintPlantOnMap(cell, plantData);
    }

    // Funcion que pinta las tiles
    private void paintPlantOnMap(Vector3Int cell, PlantsData plantData)
    {
        GameObject cropGO = new GameObject("crop_" + cell);
        cropGO.transform.parent = plantMap.transform;
        cropGO.transform.position = plantMap.CellToWorld(cell) + new Vector3(0.5f, 0.25f, 0);

        SpriteRenderer sr = cropGO.AddComponent<SpriteRenderer>();
        sr.sprite = plantData.GrowSprites[0];
        sr.sortingOrder = -7;
    }

    // Funcion que pinta pasandole un diccionario
    private void PloughWaterFromDictionary(Dictionary<Vector3Int, TileClass> tileDatMap)
    {
        foreach (var keyValue in tileDatMap)
        {
            Vector3Int cell = keyValue.Key;
            TileClass tile = keyValue.Value;

            paintTiles(cell, tile);
        }
    }
    // Funcion que pinta las tiles cogiendo la indormacion de los parametros pasados
    public void paintTiles(Vector3Int cell, TileClass tile)
    {
        if (tile.IsTilled && ploughMap != null)
            ploughMap.SetTile(cell, ploughRule); 

        if (tile.IsWatered && waterMap != null)
            waterMap.SetTile(cell, waterRule);

        //if (tile.HasCrop && plantMap != null)
        //plantMap.SetTile(cell, tile.plantToPlant);  // ← planta
    }

    // Funcion que calcula el intervalo de dias dependiendo de los dias introducidos por el usuario y el numero de sprites de crecimiento que tiene
    public void calculateGrowthDay()
    {
        foreach (var keyValue in tileDatMap)
        {
            Vector3Int cell = keyValue.Key;
            TileClass tile = keyValue.Value;

            if (tile.PlantedPlantCrop != null)
            {
                var crop = tile.PlantedPlantCrop;

                Sprite[] growSprites = crop.PlantData.GrowSprites;
                int totalStages = growSprites.Length - 2;

                TimeSpan timePassed = simulation ? TimeSpan.FromDays(simulatedDays) : currentDay - crop.DayPlanted;

                daysPassed = timePassed.Days;

                int daysSinceWatered = tile.LastDayWatered != null
                    ? (DateTime.Today - tile.LastDayWatered).Days
                    : (DateTime.Today - crop.DayPlanted).Days;

                if (daysSinceWatered > 2)
                {
                    tile.IsDead = true;

                    string cropName = "crop_" + cell;
                    Transform cropTransform = plantMap.transform.Find(cropName);
                    if (cropTransform != null)
                    {
                        Destroy(cropTransform.gameObject);
                    }

                    tile.PlantedPlantCrop = null;
                    continue;
                }

                if (totalStages <= 0 || crop.DefinedGrowthDays <= 0) continue;

                float interval = (float)crop.DefinedGrowthDays / totalStages;

                int stage = Mathf.Clamp(
                    Mathf.FloorToInt(daysPassed / interval) + 2,
                    0,
                    growSprites.Length - 1
                );

                if (stage != crop.CurrentGrowthState)
                {
                    crop.CurrentGrowthState = stage;

                    string cropName = "crop_" + cell;
                    Transform cropTransform = plantMap.transform.Find(cropName);
                    if (cropTransform != null)
                    {
                        SpriteRenderer sr = cropTransform.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            sr.sprite = growSprites[stage];
                        }
                    }
                }
            }
        }
    }

    // Funcion que se llama al cambiar de dia y actualiza las tiles
    public void NewDayUpdateWaterPlantPlough()
    {
        foreach (var keyValue in tileDatMap)
        {
            Vector3Int cell = keyValue.Key;
            TileClass tile = keyValue.Value;

            if (tile.IsWatered)
            {
                tile.IsWatered = false;
                waterMap.SetTile(cell, null);
            }

            if (tile.PlantedPlantCrop == null && !tile.IsWatered)
            {
                tile.IsTilled = false;
                ploughMap.SetTile(cell, null);
            }
        }
    }


    public void setSelectedPlant(PlantsData selectedPlant)
    {
        this.selectedPlant = selectedPlant;
    }

    public Dictionary<Vector3Int, TileClass> getTileDatMap()
    {
        return tileDatMap;
    }
}