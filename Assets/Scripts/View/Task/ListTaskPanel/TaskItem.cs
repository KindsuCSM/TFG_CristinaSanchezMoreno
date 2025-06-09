using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskItem : MonoBehaviour
{
    public TextMeshProUGUI tvTaskName;
    public TextMeshProUGUI tvDescription;
    public TextMeshProUGUI tvPlantName;

    public void SetTask(TaskModel task)
    {
        tvTaskName.text = "Task name: " + task.TaskName;
        tvDescription.text = "Task description: " + task.Description;
        tvPlantName.text = "Plant:" + task.PlantsData.PlantName;
    }
}
