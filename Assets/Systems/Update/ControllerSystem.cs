using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ControllerSystem))]
public sealed class ControllerSystem : UpdateSystem {

    private Filter _filterController;
    private Filter _filterPlayer;
    private Stash<MovableComponent> _movableStash;
    private Stash<PlayerView> _playerStash;

    Entity playerEntity;
    public override void OnAwake() {

        this._filterController = this.World.Filter.With<ControlComponent>();
        this._filterPlayer = this.World.Filter.With<PlayerView>();
        this._movableStash = this.World.GetStash<MovableComponent>();
        this._playerStash = this.World.GetStash<PlayerView>();

        playerEntity = GameState.instance.PlayerEntity;
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var item in this._filterController)
        {
            foreach (var entity in _filterPlayer)
            {
                ref var movableComponent = ref _movableStash.Get(playerEntity);
                ref var playerView = ref _playerStash.Get(playerEntity);

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerView.playerObject.transform.Translate(Vector3.left * movableComponent.MoveSpeed * deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    playerView.playerObject.transform.Translate(Vector3.right * movableComponent.MoveSpeed * deltaTime);
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    playerView.playerObject.transform.Translate(Vector3.forward * movableComponent.MoveSpeed * deltaTime);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    playerView.playerObject.transform.Translate(Vector3.back * movableComponent.MoveSpeed * deltaTime);
                }
            }
        }
    }
}