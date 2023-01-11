using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(SpawnerInit))]
public sealed class SpawnerInit : Initializer {
    public override void OnAwake() {

        var entity = this.World.CreateEntity();
        var spawnerObject = FindObjectOfType<EnemySpawnerTag>();
        ref var enemySpawner = ref entity.AddComponent<EnemySpawner>();
        enemySpawner.SpawnerObject = spawnerObject.gameObject;

        for (int i = 0; i < 10; i++)
        {
            var eventEntity = this.World.CreateEntity();

            ref var spwanEvent = ref eventEntity.AddComponent<SpawnEvent>();
        }
    }

    public override void Dispose() {
    }
}