using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskModel
{
    private string taskName;
    private int dateEnd;
    private string description;
    private DateTime datePlanted;
    private Vector3Int plantedTile;
    private PlantsData plantsData;

    public TaskModel(string taskName, int dateEnd, string description, DateTime datePlanted,Vector3Int plantedTile, PlantsData plantsData)
    {
        TaskName = taskName;
        DateEnd = dateEnd;
        Description = description;
        PlantedTile = plantedTile;
        PlantsData = plantsData;
        DatePlanted = datePlanted; 
    }

    public string TaskName
    {
        get { return taskName; }
        set { taskName = value; }
    }

    public int DateEnd
    {
        get { return dateEnd; }
        set { dateEnd = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public System.DateTime DatePlanted
    {
        get { return datePlanted; }
        set { datePlanted = value; }
    }

    public Vector3Int PlantedTile
    {
        get { return plantedTile; }
        set { plantedTile = value; }
    }

    public PlantsData PlantsData
    {
        get { return plantsData; }
        set { plantsData = value; }
    }

    public override string ToString()
    {
        return $"TaskName: {TaskName}, " +
                $"DateEnd: {DateEnd}, " +
                $"Description: {Description}, " +
                $"PlantedTile: ({PlantedTile.x}, {PlantedTile.y}, {PlantedTile.z}), " +
                $"PlantsData: {(PlantsData != null ? PlantsData.name : "null")}";
    }

}


