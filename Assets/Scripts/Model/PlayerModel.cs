using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public Vector2 SpawnPosition { get; private set; } = new Vector2(6.5f, 16.5f);
    public float MoveSpeed = 3f;
    public float MoveRunningSpeed = 20f;
    private int playerMoney = 400;

    public Vector2 LastPosition { get; set; } = Vector2.down;
    public bool IsRunning { get; set; }

    public int PlayerMoney
    {
        get { return playerMoney; }
        set { playerMoney = value; }
    }
    
}