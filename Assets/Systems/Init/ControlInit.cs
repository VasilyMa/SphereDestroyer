using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(ControlInit))]
public sealed class ControlInit : Initializer {
    public override void OnAwake() {

        var entity = this.World.CreateEntity(); 

        ref var controlComponent = ref entity.AddComponent<ControlComponent>();

        Debug.Log(entity.ID);
    }

    public override void Dispose() {
    }
}