using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
public class EnemyConfig : Config
{
    public GameObject WeakEnemy;
    public GameObject MediumEnemy;
    public GameObject StrongEnemy;
}
