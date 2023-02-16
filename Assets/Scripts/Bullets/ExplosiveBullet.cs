using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Models;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    private List<GameObject> _enemiesInside;
    private AudioClip _dieSound;
    public AudioSource audioSource;

    [SerializeField]
    public int modifierTickDuration;
    [SerializeField]
    public float modifierFreezeScale;

    // private CannonCleaveCollider cleaveCollider;
    private CircleCollider2D bulletCollider;
    private CircleCollider2D _cleaveCollider;
    private ParticleSystem _ps;

    // protected override void Start()
    // {
    //     base.Start();
    //     
    //     //_audioSource = GetComponent<AudioSource>();
    // }
    protected void Awake()
    {
        _enemiesInside = new List<GameObject>();
        bulletCollider = GetComponent<CircleCollider2D>();
        _dieSound = Resources.Load<AudioClip>("Audio/Explosion_Die");
        _ps = GetComponentInChildren<ParticleSystem>();
        _cleaveCollider = GetComponentsInChildren<CircleCollider2D>()[1];
    }

    protected override void MoveToEnemy()
    {
        var position = this.transform.position;
        if (EnemyExists) Prevpos = targetEnemy.transform.position;
        else
        {
            //todo move VectorEquals to somewhere else (definitely not creepController)
            if (CreepController.VectorEquals(position, Prevpos))
            {
                var e = new CreepController();
                //because in EnemyReached() ref CreepController is not used but required.
                EnemyReached(ref e);
                return;
            }
        }
        Vector2 enemyPos;
        if (EnemyExists)
            enemyPos = targetEnemy.transform.position;
        else
            enemyPos = Prevpos;
        
        var dest2 = Vector2.MoveTowards(position, enemyPos, speed * Time.deltaTime);
        var dest = (Vector3) dest2;
        dest.z = -2.5f;
        this.transform.position = dest;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (targetEnemy == null) return;
        if (col.CompareTag("Enemy"))
        {
            if (col.gameObject == targetEnemy)
            {
                var creep = targetEnemy.GetComponent<CreepController>();

                EnemyReached(ref creep);
            }
        }
    }

    public void CleaveEnemyEnter(Collider2D c) => EnemyTrigger(c);
    public void CleaveEnemyExit(Collider2D c) => EnemyTriggerExit(c);

    protected override void EnemyTriggerExit(Collider2D col)
    {
        _enemiesInside.Remove(col.gameObject);
    }
    protected override void EnemyTrigger(Collider2D col)
    {
        if (!_enemiesInside.Contains(col.gameObject))
        {
            _enemiesInside.Add(col.gameObject);
        }
    }

    protected override bool EnemyReached(ref CreepController creep)
    {
        var objs = Physics2D.OverlapCircleAll(this.transform.position, _cleaveCollider.radius).ToList();
        var creeps = objs.Where(x => x.gameObject.CompareTag("Enemy")).ToList();
        for (var i = 0; i < creeps.Count; i++)
        {
            var obj = creeps[i];
            var _creep = obj.GetComponent<CreepController>();
            var explodeSlowModifier = new ExplodeSlowModifier(modifierTickDuration, _creep, modifierFreezeScale);
            _creep.ApplyModifier(explodeSlowModifier);
            _creep.TakeDamage(this.attackDamage);
        }
        
        
        audioSource.PlayOneShot(_dieSound);
        //_ps.Play();
        Destroy(this.gameObject);
        return true;
    }
}
