using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    [SerializeField]
    public int Health { get; set; }
    [SerializeField]
    public int Money { get; set; }

    [SerializeField]
    private int initialMoney;

    [SerializeField]
    private int initialHealth;

    private int totalMoney = 0;
    private int towersBuilded = 0;
    private int upgradesCost = 0;
    private int creepsDied = 0;
    private int _maxHealth;
    
    public static GlobalVars current;

    private void Awake()
    {
        Health = initialHealth <= 0 ? 15 : initialHealth;
        Money = initialMoney <= 0 ? 40 : initialMoney;

        current = this;
    }

    public int GetTotalMoney()
    {
        return totalMoney;  
    }

    public void Start()
    {
        _maxHealth = Health;
        GameEvents.current.OnEnemyEnterBase += HealthDecrease;
        GameEvents.current.OnTowerBuilt += () => towersBuilded++;
        GameEvents.current.OnUpgraded += (int cost) => upgradesCost += cost;
        GameEvents.current.OnCreepDied += () => creepsDied++;
    }

    public string[] GetStats()
    {
        return new[] {totalMoney.ToString(), upgradesCost.ToString(), (_maxHealth - Health).ToString(), towersBuilded.ToString(), creepsDied.ToString()};
    }

    public void HealthDecrease()
    {
        Health--;
        if (Health <= 0)
        {
            GameEvents.current.Lose();
        }
        GameEvents.current.HealthDecreased();
    }

    public void AddMoney(int value)
    {
        this.Money += value;
        totalMoney += value;
    }

    public void DecreaseMoney(int value)
    {
        this.Money -= value;
    }
    
}
