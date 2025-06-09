using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListTaskPanel : MonoBehaviour
{
    public Button btnOpenCloseWindow;
    public RectTransform taskListPanel;
    public TaskList taskList;
    public GameObject taskItemPrefab;
    public Transform contentParent;
    private bool isActive;

    void Start()
    {
        taskList = taskList = GameManager.Instance.taskList;
        setListeners();
        UpdateFillList();
    }

    private void setListeners()
    {
        btnOpenCloseWindow.onClick.AddListener(() => OnOpenCloseWindow());
    }

    private void OnOpenCloseWindow()
    {
        isActive = !isActive;
        Vector2 windowPosition = taskListPanel.anchoredPosition;
        if (!isActive)
        {
            windowPosition.x = 881f;
        }
        else
        {
            windowPosition.x = 291f;
        }
        taskListPanel.anchoredPosition = windowPosition;
    }

    public void UpdateFillList()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (TaskModel task in taskList.getTaskList())
        {
            GameObject taskGO = Instantiate(taskItemPrefab, contentParent);
            TaskItem taskItem = taskGO.GetComponent<TaskItem>();

            if (taskItem != null)
            {
                taskItem.SetTask(task);
            }
        }
    }

}
