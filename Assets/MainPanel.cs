using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Scellecs.Morpeh;

public class MainPanel : MonoBehaviour
{
    private CompositeDisposable disposable = new CompositeDisposable();

    public IntReactiveProperty KillCounter = new IntReactiveProperty();
    public IntReactiveProperty MoveSpeed = new IntReactiveProperty();
    public IntReactiveProperty DamagePerSecond = new IntReactiveProperty();
    public IntReactiveProperty AttackRange = new IntReactiveProperty();

    public Text killCount;
    public Text moveSpeed;
    public Text DPS;
    public Text attackRange;

    public Button upgrade;
    void Start()
    {
        upgrade.OnClickAsObservable().Subscribe(value =>
        {
            var entity = World.Default.CreateEntity();

            entity.AddComponent<UpgradeComponent>();

        }).AddTo(disposable);


        KillCounter.Subscribe(value => 
        {
            killCount.text = $"Kill count: {KillCounter.Value}";
        }).AddTo(disposable);

        MoveSpeed.Subscribe(value =>
        {
            moveSpeed.text = $"Move speed: {MoveSpeed.Value}";
        }).AddTo(disposable);

        DamagePerSecond.Subscribe(value =>
        {
            DPS.text = $"DPS: {DamagePerSecond.Value}";
        }).AddTo(disposable);

        AttackRange.Subscribe(value =>
        {
            attackRange.text = $"Attack range: {AttackRange.Value}";
        }).AddTo(disposable);
    }

    void Update()
    {
        
    }
}
