using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UpgradeSystem))]
public sealed class UpgradeSystem : UpdateSystem 
{
    private Filter _filter;

    private Stash<PlayerView> _playerStash;
    private Stash<DamageComponent> _damageStash;
    private Stash<MovableComponent> _movableStash;
    public override void OnAwake() 
    {
        this._filter = this.World.Filter.With<UpgradeComponent>();

        this._playerStash = this.World.GetStash<PlayerView>();
        this._damageStash = this.World.GetStash<DamageComponent>();
        this._movableStash = this.World.GetStash<MovableComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in this._filter)
        {
            ref var movableComponent = ref _movableStash.Get(GameState.instance.PlayerEntity);
            ref var damageComponent = ref _damageStash.Get(GameState.instance.PlayerEntity);

            Upgrade(ref movableComponent, ref damageComponent);

            entity.RemoveComponent<UpgradeComponent>();
        }
    }

    private void Upgrade(ref MovableComponent movableComponent, ref DamageComponent damageComponent)
    {
        var random = UnityEngine.Random.Range(0, 3);
        var chance = UnityEngine.Random.Range(0, 101);
        switch (random)
        {
            case 0:
                if (chance <= GameState.instance.PlayerConfig.ChanceToUpgradeRangeAttack)
                {
                    damageComponent.RangeAttack += GameState.instance.PlayerConfig.StepToUpgradeRangeAttack;
                    GameState.instance.MainPanel.AttackRange.Value = (int)damageComponent.RangeAttack;
                    return;
                }
                break;
            case 1:
                if (chance <= GameState.instance.PlayerConfig.ChanceToUpgradeDPS)
                {
                    damageComponent.DamagePerSecond += GameState.instance.PlayerConfig.StepToUpgradeDPS;
                    GameState.instance.MainPanel.DamagePerSecond.Value = damageComponent.DamagePerSecond;
                    return;
                }
                break;
            case 2:
                if (chance <= GameState.instance.PlayerConfig.ChanceToUpgradeMoveSpeed)
                {
                    movableComponent.MoveSpeed += GameState.instance.PlayerConfig.StepToUpgradeMoveSpeed;
                    GameState.instance.MainPanel.MoveSpeed.Value = (int)movableComponent.MoveSpeed;
                    return;
                }
                break;
            default:
                break;
        }
        Upgrade(ref movableComponent, ref damageComponent);
    }
}