using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController playerController;
    public TileManager tileManager;
    public GameObject inventoryPanel;
    public Button btnSave;
    public Button btnEraseJSON;

    public TaskList taskList = new TaskList();

    // En Awake indicamos a unity que en el cambio de escena no se eliminará el game object al que el gameobject esté asignado ni sus gameobjects hijos
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // iniciamos la lista de las plantas
        ItemList.initList();
        Debug.Log("Plantas cargadas: " + ItemList.getList().Count);

        // Cargamos el juego desde el ultimo guardado
        WorldSave.Instance.LoadGame();
        SetButtonSaveListener(); // Inicializamos el btn con su listener para guardar partida
        SetButtonEraseListener();
    }


    // En caso de que colisione con una planta se notifica
    public void OnSeedCollision(GameObject seed)
    {
        Debug.Log($"Semilla recogida: {seed.name}");
    }

    // Funcion que enviará al tilemanager la función que realiza el jugador
    public void OnActionButton(string action)
    {
        Vector3Int targetCell = playerController.GetTargetCell();

        if (action == "water" || action == "plough")
        {
            tileManager.PerformTileAction(action, targetCell);
        }
        else if (action == "plant")
        {
            tileManager.PerformTileAction(action, targetCell);
        }
    }

    // Creacion del listener para el btn de guardar el mundo
    private void SetButtonSaveListener()
    {
        btnSave.onClick.AddListener(() => WorldSave.Instance.SaveGame());

    }

    private void SetButtonEraseListener()
    {
        btnEraseJSON.onClick.AddListener(() => WorldSave.Instance.DeleteFile());
    }
}
