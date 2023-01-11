using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System.Collections.Generic;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AttackSystem))]
public sealed class AttackSystem : UpdateSystem {
    private Filter _fitler;

    private Stash<DamageComponent> _damageStash;
    private Stash<PlayerView> _playerStash;


    public override void OnAwake()
    {
        this._fitler = this.World.Filter.With<PlayerView>();
        this._damageStash = this.World.GetStash<DamageComponent>();
        this._playerStash = this.World.GetStash<PlayerView>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this._fitler)
        {
            ref var damageComponent = ref _damageStash.Get(entity);
            ref var viewComponent = ref _playerStash.Get(entity);
            List<EnemyTag> enemies = new List<EnemyTag>();
            List<EnemyTag> nearestEnemies = new List<EnemyTag>();
            EnemyTag closet = null;

            if (damageComponent.DamageTick > 0)
            {
                damageComponent.DamageTick -= Time.deltaTime;
                return;
            }
            else
                damageComponent.DamageTick = 0;

            Collider[] hitColliders = new Collider[GameState.instance.MaxEnemies];

            int numColliders = Physics.OverlapSphereNonAlloc(viewComponent.playerObject.transform.position, damageComponent.RangeAttack, hitColliders, GameState.instance.EnemyLayer);

            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].CompareTag("Enemy"))
                {
                    if (hitColliders[i].TryGetComponent<EnemyTag>(out var enemy))
                    {
                        enemies.Add(enemy);
                    }
                }
            }

            FindNearestEnemies(ref viewComponent, enemies, nearestEnemies, closet);

            foreach (var nearest in nearestEnemies)
            {
                var eventEntity = this.World.CreateEntity();

                ref var damageEvent = ref eventEntity.AddComponent<DamageEvent>();
                damageEvent.Damage = damageComponent.DamagePerSecond;
                damageEvent.EntityTarget = nearest.Entity;

#if UNITY_EDITOR
                nearest.Health -= damageComponent.DamagePerSecond;
#endif

                Debug.Log($"Detected enemy is {nearest.gameObject.GetInstanceID()}ID and damage it with {damageComponent.DamagePerSecond} amount");
            }

            damageComponent.DamageTick = 1;
        }
    }
    void FindNearestEnemies(ref PlayerView playerView, List<EnemyTag> enemies, List<EnemyTag> nearestEnemies, EnemyTag closet)
    {
        for (int i = 0; i < 3; i++)
        {
            float distance = Mathf.Infinity;
            Vector3 position = playerView.playerObject.transform.position;
            foreach (var enemy in enemies)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closet = enemy;
                    distance = curDistance;
                }
            }

            enemies.Remove(closet);
            if (closet != null) nearestEnemies.Add(closet);
                if (closet != null) closet = null;
        }

    }
}