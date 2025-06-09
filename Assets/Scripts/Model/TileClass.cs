using System;


public class TileClass
{
    private bool isTilled;
    private bool isWatered;
    private bool hasCrop;
    public DateTime lastWateredDay;
    public bool isDead;
    private int growthStage;
    private PlantedPlantCrop plantedPlantCrop;

    public TileClass()
    {
        isTilled = false;
        isWatered = false;
        hasCrop = false;
        growthStage = 0;
    }

    public bool IsTilled
    {
        get { return isTilled; }
        set { isTilled = value; }
    }

    public bool IsWatered
    {
        get { return isWatered; }
        set { isWatered = value; }
    }

    public bool HasCrop
    {
        get { return hasCrop; }
        set { hasCrop = value; }
    }

    public int GrowthStage
    {
        get { return growthStage; }
        set { growthStage = value; }
    }

    public PlantedPlantCrop PlantedPlantCrop
    {
        get { return plantedPlantCrop; }
        set { plantedPlantCrop = value; }
    }

    public DateTime LastDayWatered
    {
        get { return lastWateredDay; }
        set { lastWateredDay = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
}
