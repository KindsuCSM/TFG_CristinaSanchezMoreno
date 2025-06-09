using System;
using UnityEngine;

public class TaskPanelController : MonoBehaviour
{
    // Variables
    public TaskPanelView view;
    public TileManager tileManager;
    public InventoryController inventory;
    public PlayerController playerController;

    private Vector3Int selectedTile;
    private TaskList taskList;
    private ListTaskPanel listPanel;


    void Start()
    {
        taskList = GameManager.Instance.taskList; // Obtenemos la lista
        listPanel = GameObject.Find("TaskListPanel").GetComponent<ListTaskPanel>(); // Obtenemos el panel en escena
        
        // Añadimos los listeners
        view.btnClose.onClick.AddListener(view.Close);
        view.btnCancel.onClick.AddListener(() =>
        {
            view.ClearFields();
            view.Close();
        });
        view.btnSave.onClick.AddListener(OnSaveClicked);
    }

    // Funcion para abrir o cerrar el panel
    public void OpenWindow(Vector3Int tile)
    {
        selectedTile = tile;
        view.Open();
    }

    // Funcion que se llama al guardar el task 
    private void OnSaveClicked()
    {
        TaskModel task = CreateTask(); //Creamos la task

        if (task == null) return;

        tileManager.plantAction(tileManager.getTilesDat(selectedTile), selectedTile, task.DateEnd); // Realizamos la plantacion
        taskList.addTask(ConvertToTaskClass(task)); // Añadimos la task
        listPanel.UpdateFillList(); // Actualizamos las task del panel

        Debug.Log($"Tarea añadida correctamente. Total tareas: {taskList.getTaskListCount()}");
        view.Close(); // Cerramos el panel
    }

    // Creamos la task
    private TaskModel CreateTask()
    {
        PlantsData selectedPlant = inventory.GetSelectedPlantData();

        if (selectedPlant == null)
        {
            Debug.LogError("planta no seleccionada");
            return null;
        }
        return new TaskModel(
            view.GetTaskName(),
            view.GetTaskDays(),
            view.GetTaskDescription(),
            DateTime.Today,
            selectedTile,
            inventory.GetSelectedPlantData()
        );
    }

    // Convertimos la task
    private TaskModel ConvertToTaskClass(TaskModel model)
    {
        return new TaskModel(model.TaskName, model.DateEnd, model.Description, model.DatePlanted, model.PlantedTile, model.PlantsData);
    }
}
