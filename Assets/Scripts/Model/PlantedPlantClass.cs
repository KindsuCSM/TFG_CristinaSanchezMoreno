using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantedPlantCrop
{
    private PlantsData plantData;
    private DateTime dayPlanted;
    private int definedGrowthDays;
    private int currentGrowthState;

    public PlantsData PlantData
    {
        get { return plantData; }
        set { plantData = value; }
    }
    public DateTime DayPlanted
    {
        get { return dayPlanted; }
        set { dayPlanted = value; }
    }
    public int DefinedGrowthDays
    {
        get { return definedGrowthDays; }
        set { definedGrowthDays = value; }
    }
    public int CurrentGrowthState
    {
        get { return currentGrowthState; }
        set { currentGrowthState = value; }
    }
}
