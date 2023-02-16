using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricParticlesScript : MonoBehaviour
{
    private TeslaTowerController _parent;
    
    private float _electricTickDamage;
    private float _electricSpeedScale;

    void Start()
    {
        _parent = GetComponentInParent<TeslaTowerController>();
    }

    private void Update()
    {
        (float damage, float speedScale) inf = _parent.GetElectricInfluence();
        _electricTickDamage = inf.damage;
        _electricSpeedScale = inf.speedScale;
    }

    void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
 
        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
 
        // get
        var colldata = new ParticleSystem.ColliderData();
        var numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out colldata);
 
        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            // instantiate the Game Object
            var obj = colldata.GetCollider(i, 0).gameObject;
            var script = obj.GetComponent<CreepController>();
            //script.Electric(_electricSpeedScale, _electricTickDamage, _parent);
            enter[i] = p;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }
    // private void OnParticleTrigger()
    // {
    //     var ps = GetComponent<ParticleSystem>();
    //     var enter = new List<ParticleSystem.Particle>();
    //     var colldata = new ParticleSystem.ColliderData();
    //     int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter, out colldata);
    //     var enemyGO = colldata.GetCollider(0, 0).gameObject;
    //     var enemy = enemyGO.GetComponent<CreepController>();
    //     
    //     Debug.Log($"ENEMY {enemyGO.name}");
    // }
}
