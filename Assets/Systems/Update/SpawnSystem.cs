using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using System;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SpawnSystem))]
public sealed class SpawnSystem : UpdateSystem {

    private Filter _spwanerFilter;
    private Filter _spawnEvent;


    public override void OnAwake() {

        this._spwanerFilter = this.World.Filter.With<EnemySpawner>();
        this._spawnEvent = this.World.Filter.With<SpawnEvent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in this._spwanerFilter)
        {
            ref var spawner = ref entity.GetComponent<EnemySpawner>();

            foreach (var evt in this._spawnEvent)
            {
                ref var spawnEvent = ref evt.GetComponent<SpawnEvent>();
                SpawnEnemy(ref spawner);
                evt.RemoveComponent<SpawnEvent>();
            }
        }
    }

    private void SpawnEnemy(ref EnemySpawner enemySpawner)
    {
        float randomX = UnityEngine.Random.Range(-10, 10);
        float randomZ = UnityEngine.Random.Range(-10, 10);
        int randomEnemy = UnityEngine.Random.Range(0, 3);
        GameObject newEnemy = null;

        switch (randomEnemy)
        {
            case 0:
                newEnemy = Instantiate(GameState.instance.EnemyConfig.WeakEnemy,
                    new Vector3(randomX, 0, randomZ),
                    Quaternion.identity,
                    enemySpawner.SpawnerObject.transform);
                break;
            case 1:
                newEnemy = Instantiate(GameState.instance.EnemyConfig.MediumEnemy,
                    new Vector3(randomX, 0, randomZ),
                    Quaternion.identity,
                    enemySpawner.SpawnerObject.transform);
                break;
            case 2:
                newEnemy = Instantiate(GameState.instance.EnemyConfig.StrongEnemy,
                    new Vector3(randomX, 0, randomZ),
                    Quaternion.identity,
                    enemySpawner.SpawnerObject.transform);
                break;
            default:
                break;
        }

        var enemyEntity = this.World.CreateEntity();
        newEnemy.GetComponent<EnemyTag>().Entity = enemyEntity;
        ref var enemy = ref enemyEntity.AddComponent<EnemyComponent>();
        enemy.EnemyObject = newEnemy;
        ref var healthComponent = ref enemyEntity.AddComponent<HealthComponent>();
        healthComponent.HealthPoints = newEnemy.GetComponent<EnemyTag>().Health;
    }
}