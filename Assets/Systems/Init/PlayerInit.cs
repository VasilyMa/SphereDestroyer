using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerInit))]
public sealed class PlayerInit : Initializer
{
    public override void OnAwake()
    {
        var entity = this.World.CreateEntity();

        GameState.instance.PlayerEntity = entity;

        var playerObject = FindObjectOfType<PlayerTag>();

        ref var healthComponent = ref entity.AddComponent<HealthComponent>();
        ref var movableComponent = ref entity.AddComponent<MovableComponent>();
        ref var playerViewComponent = ref entity.AddComponent<PlayerView>();
        ref var damageComponent = ref entity.AddComponent<DamageComponent>();

        playerViewComponent.playerObject = playerObject.gameObject;
        healthComponent.HealthPoints = 100;

        movableComponent.MoveSpeed = GameState.instance.PlayerConfig.MoveSpeed;
        damageComponent.DamagePerSecond = GameState.instance.PlayerConfig.DamagePerSecond;
        damageComponent.RangeAttack = GameState.instance.PlayerConfig.RangeAttack;
    }

    public override void Dispose() {
    }
}