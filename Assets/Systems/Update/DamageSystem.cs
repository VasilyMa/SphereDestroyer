using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem {

    private Filter filter;
    private Stash<DamageEvent> _damageStash;
    private Stash<HealthComponent> _healthStash;
    private Stash<EnemyComponent> _enemyStash;

    Entity EventEntity;
    public override void OnAwake() {

        this.filter = this.World.Filter.With<DamageEvent>();
        this._damageStash = this.World.GetStash<DamageEvent>();
        this._healthStash = this.World.GetStash<HealthComponent>();
        this._enemyStash = this.World.GetStash<EnemyComponent>();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var entity in this.filter)
        {
            EventEntity = entity;
            ref var damageComponent = ref _damageStash.Get(entity);

            if (damageComponent.EntityTarget == null) DeleteEvent();

            DoDamage(ref damageComponent);

            DeleteEvent();
        }
    }
    private void DoDamage(ref DamageEvent damageComponent)
    {
        ref var healthComponent = ref _healthStash.Get(damageComponent.EntityTarget);

        healthComponent.HealthPoints -= damageComponent.Damage;

        if (healthComponent.HealthPoints <= 0) DieEvent(damageComponent.EntityTarget);
    }
    private void DieEvent(Entity entity)
    {
        ref var enemy = ref _enemyStash.Get(entity);
        enemy.EnemyObject.SetActive(false);

        entity.Dispose();

        var eventEntity = this.World.CreateEntity();

        ref var spwanEvent = ref eventEntity.AddComponent<SpawnEvent>();

        GameState.instance.MainPanel.KillCounter.Value++;
    }
    void DeleteEvent()
    {
        EventEntity.RemoveComponent<DamageEvent>();
        EventEntity = null;
    }
}