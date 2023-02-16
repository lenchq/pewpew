using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bullets
{
    public class PenetrativeBullet : Bullet
    {
        private List<GameObject> DealtDamage;

        public float penetration = 0f;
        private bool ricochet = false;
        
        protected override void Start()
        {
            base.Start();
            DealtDamage = new List<GameObject>();
        }

        protected override void EnemyTrigger(Collider2D col)
        {
            if (penetration > 0f && !DealtDamage.Contains(col.gameObject))
            {
                var creep = col.gameObject.GetComponent<CreepController>();
                creep.TakeDamage(this.attackDamage * penetration);
                DealtDamage.Add(col.gameObject);
            }
        }
        
        protected override void OnTriggerStay2D(Collider2D col)
        {
            if (targetEnemy == null) return;
            if (col.CompareTag("Enemy"))
            {
                if (col.gameObject == targetEnemy)
                {
                    var creep = targetEnemy.GetComponent<CreepController>();

                    var destroy = EnemyReached(ref creep);

                    if (destroy) Destroy(this.gameObject);
                }
            }
        }

        protected override bool EnemyReached(ref CreepController creep)
        {
            if (ricochet)
                creep.TakeDamage(attackDamage * penetration);
            else
                creep.TakeDamage(this.attackDamage);
            if (penetration > 0f && !ricochet)
            {
                var objs = Physics2D.OverlapCircleAll(this.transform.position, 1f);
                var creepObjs = objs.Where(x => x.CompareTag("Enemy")).ToList();
                creepObjs.Remove(creep.GetComponent<Collider2D>());
                var ric_creep = NearestEnemy(creepObjs);
                
                if (ric_creep is null)
                    return true;
                
                targetEnemy = ric_creep;
                ricochet = true;
                return false;
            }
            return true;
        }
        private GameObject NearestEnemy(List<Collider2D> Colliders)
        {
            var position = this.transform.position;
            if (Colliders.Count == 0)
            {
                return null;
            }
            else if (Colliders.Count == 1)
            {
                return Colliders[0].gameObject;
            }
            else if (Colliders.Count > 1)
            {
                var distance = float.MaxValue;
                Collider2D closest = new Collider2D();
                for (var i = 0; i < Colliders.Count; i++)
                {
                    var colDist = Vector3.Distance(position, Colliders[i].gameObject.transform.position);
                    if (colDist < distance)
                    {
                        distance = colDist;
                        closest = Colliders[i];
                    }
                }
                return closest.gameObject;
            }

            return null;
        }
    }
}