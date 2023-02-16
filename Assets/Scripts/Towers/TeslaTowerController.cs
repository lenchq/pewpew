using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using Mono.Cecil;
using UnityEngine;
public class TeslaTowerController : TowerController
{
    
    // [SerializeField]
    // public float attackFrequencyTicks = 1;

    [SerializeField]
    public float electricFreezeScale = .3f;

    [Header("Upgrade")]
    public float electricFreezeUpgradeDifference = .05f;

    private ParticleSystem _ps;
    private int _tickCounter;
    [SerializeField]
    private int electricTickLength = 25;
    protected override float RotationTolerance => 45;

    protected override void Start()
    {
        base.Start();
        _ps = GetComponentInChildren<ParticleSystem>();
        OnAdditionalUpgradeTextFormat += AdditionalUpgradeTextFormat;
        OnUpgrade += Upgrade;
    }

    private string AdditionalUpgradeTextFormat()
    {
        var format = $"Замедление: {electricFreezeScale:P1} + {electricFreezeUpgradeDifference}";
        return format;
    }

    private new void Upgrade()
    {
        this.electricFreezeScale += electricFreezeUpgradeDifference;
    }

    public override void TriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!Colliders.Contains(other))
            {
                Colliders.Add(other);
                _ps.trigger.AddCollider(other);
            }
        }
    }

    public override void TriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Colliders.Contains(other))
            {
                Colliders.Remove(other);
                _ps.trigger.RemoveCollider(other);

                if (other.gameObject == SelectedEnemy)
                {
                    SelectedEnemy = null;
                }
            }
        }
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (SelectedEnemy is not null)
        {
            PerformShoot();
            //! Code to deal damage one time in <attackFrequencyTicks> ticks. Unused.
            // if (_tickCounter >= this.attackFrequencyTicks)
            // {
            //     PerformShoot();
            //     _tickCounter = 0;
            // }
            // else _tickCounter++;
        }
        else
        {
            _ps.Stop();
            Rotated = false;
            SelectEnemy();
        }
        
        FixAttackRadius();
    }
    
    protected override void PerformShoot()
    {
        if (SelectedEnemy == null) return;

        if (Rotated)
        {
            if (!_ps.isPlaying)
                _ps.Play();
            var creep = SelectedEnemy.GetComponent<CreepController>();

            ElectricModifier mod =
                new ElectricModifier(attackDamage, electricFreezeScale, electricTickLength, creep);
            
            creep.ApplyModifier(mod);
            
        }
    }
    public (float damage, float speedScale) GetElectricInfluence()
    {
        return (damage: this.attackDamage, speedScale: this.electricFreezeScale);
    }
}
