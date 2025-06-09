using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public PlayerData playerData;
    public List<TaskData> tasks;
    public List<TileData> tiles;
    public List<InventorySlotData> inventorySlots;
}

[Serializable]
public class PlayerData
{
    public int playerMoney;
}

[Serializable]
public class TaskData
{
    public string taskName;
    public int taskDateEnd;
    public string taskDescription;
    public DateTime taskDatePlanted;
    public int tileX;
    public int tileY;
    public int tileZ;
    public string plantName;
}

[Serializable]
public class TileData
{
    public int tileX;
    public int tileY;
    public int tileZ;

    public bool isTilled;
    public bool isWatered;
    public bool hasCrop;
    public int growthStage;
    public bool isDead;
    public string plantedPlantName;
    public long lastWateredTicks;
}

[Serializable]
public class InventorySlotData
{
    public string plantName;

}


