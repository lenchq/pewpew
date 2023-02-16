using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameEvents current;
    private event Action OnHealthDecreased;
    private event Action<int> OnAddMoney;
    public event Action OnEnemyEnterBase;
    public event Action OnWin;
    public event Action OnLose;
    public event Action OnTowerBuilt;
    public event Action<int> OnUpgraded;
    public event Action OnCreepDied;

    private void Awake()
    {
        current = this;
    }

    public void CreepDied()
    {
        OnCreepDied?.Invoke();
    }
    public void Upgraded(int cost)
    {
        OnUpgraded?.Invoke(cost);
    }
    public void EnemyEnterBase()
    {
        OnEnemyEnterBase?.Invoke();
    }

    public void TowerBuilt()
    {
        OnTowerBuilt?.Invoke();
    }

    public void Win()
    {
        OnWin?.Invoke();
    }

    public void Lose()
    {
        OnLose?.Invoke();
    }

    public void AddMoney(int value)
    {
        OnAddMoney?.Invoke(value);
    }

    public void HealthDecreased()
    {
        OnHealthDecreased?.Invoke();
    }
}
