using System;
using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

public class BulletCleaveCollider : MonoBehaviour
{
    private Collider2D _collider;
    private ExplosiveBullet _parent;
    void Awake()
    {
        _parent = GetComponentInParent<ExplosiveBullet>();
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _parent.CleaveEnemyEnter(col);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        _parent.CleaveEnemyExit(col);
    }
}
