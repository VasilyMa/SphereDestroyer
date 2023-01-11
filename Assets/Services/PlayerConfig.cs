using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : Config
{
    [Header("Start stats")]
    public float MoveSpeed;
    public int DamagePerSecond;
    public float RangeAttack;
    [Space(25)]
    [Header("Upgrade")]
    public float StepToUpgradeMoveSpeed;
    public int ChanceToUpgradeMoveSpeed;
    [Space(10)]
    public int StepToUpgradeDPS;
    public int ChanceToUpgradeDPS;
    [Space(10)]
    public float StepToUpgradeRangeAttack;
    public int ChanceToUpgradeRangeAttack;
}
