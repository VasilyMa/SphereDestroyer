using Scellecs.Morpeh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour //There contains are Game states: player entity and other
{
    public EnemyConfig EnemyConfig;
    public PlayerConfig PlayerConfig; //throw inspector can customise stats of player

    public Installer EcsInstaller;

    public Entity PlayerEntity;
    public Entity Controller;
    public LayerMask EnemyLayer;
    public int MaxEnemies; //there max enemies in level from start

    public MainPanel MainPanel;

    public static GameState instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
        { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
