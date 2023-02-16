using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameEvents.current.EnemyEnterBase();
        var creep = other.gameObject.GetComponent<CreepController>();
        creep.Die(false);
    }
}
