using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList
{
    private List<TaskModel> tasks = new List<TaskModel>();

    public void addTask(TaskModel task)
    {
        tasks.Add(task);
    }
    public int getTaskListCount()
    {
        return tasks.Count;
    }

    public List<TaskModel> getTaskList()
    {
        return tasks;
    }

}
