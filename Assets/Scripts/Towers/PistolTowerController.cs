using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bullets;
using Towers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class PistolTowerController : ReloadableTower
{
    // [SerializeField]
    private GameObject _bullet;

    [Header("Stats")]
    [SerializeField]
    public float bulletSpeed = 4f;
    [SerializeField]
    public float penetrationScale = 0f;

    [Header("Upgrade")]
    [SerializeField]
    public float bulletSpeedUpgradeScale = .1f;
    public float penetrationScaleUpgrade = .1f;
    private Transform _bulletSpawn;

    protected override void Start()
    {
        base.Start();
        _bulletSpawn = GetComponentsInChildren<Transform>()[2];
        _bullet = Resources.Load("Prefabs/PistolBullet") as GameObject;
        OnAdditionalUpgradeTextFormat += AdditionalUpgradeTextFormat;
        OnUpgrade += Upgrade;
    }

    private string AdditionalUpgradeTextFormat()
    {
        var w = $"Пробитие: {penetrationScale:P1} + {(penetrationScale + penetrationScaleUpgrade):P1}";
        return w;
    }

    protected override void Update()
    {
        if (SelectedEnemy is not null)
        {
            PerformShoot();
        }
        else
        {
            Rotated = false;
            SelectEnemy();
            
        }
        
        FixAttackRadius();
    }
    protected override void PerformShootWithReload()
    {
        // var gun = GetComponentsInChildren<Transform>()[2];
        //Vector3 bulletPosition = ((Vector2)this.transform.position - new Vector2(0, -0.5f));
        
        // var bulletInstance = (GameObject) Instantiate(bullet.gameObject,
        //     bulletPosition, Quaternion.identity, gun);
        var bulletInstance = Instantiate(_bullet,
            _bulletSpawn.position, _bulletSpawn.rotation);
        AudioPlayer.PlayOneShot(GetRandomShootSound());

        var script = bulletInstance.GetComponent<PenetrativeBullet>();
        script.speed = bulletSpeed;
        script.penetration = penetrationScale;
        script.attackDamage = attackDamage;
        script.targetEnemy = SelectedEnemy;
    }

    private new void Upgrade()
    {
        this.bulletSpeed += bulletSpeed * bulletSpeedUpgradeScale;
        this.penetrationScale += penetrationScaleUpgrade;
    }
}
