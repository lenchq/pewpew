using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BoxCollider2D _trigger;

    public GameObject targetEnemy;
    public float speed;
    public float attackDamage;
    public float size = .2f;

    // protected bool Freemove;
    protected Vector2 Prevpos;
    protected bool Destroyed;
    // protected Vector2 Parpos;
    protected bool EnemyExists = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.transform.localScale = new Vector2(size, size);
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyTriggerExit(col);
        }
    }

    protected virtual void EnemyTriggerExit(Collider2D col)
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (targetEnemy == null) return;
        if (col.CompareTag("Enemy"))
        {
            EnemyTrigger(col);

            if (col.gameObject == targetEnemy)
            {
                var creep = targetEnemy.GetComponent<CreepController>();

                var destroy = EnemyReached(ref creep);

                if (destroy) Destroy(this.gameObject);
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        // if (targetEnemy == null) return;
        // if (col.CompareTag("Enemy"))
        // {
        //     if (col.gameObject == targetEnemy)
        //     {
        //         var creep = targetEnemy.GetComponent<CreepController>();
        //
        //         var destroy = EnemyReached(ref creep);
        //
        //         if (destroy) Destroy(this.gameObject);
        //     }
        // }
    }

    protected virtual void EnemyTrigger(Collider2D col)
    {
        
    }

    protected virtual bool EnemyReached(ref CreepController creep)
    {
        return true;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (Destroyed)
        {
            this.Die();
            return;
        }

        // go to current tower's target if its disappeared. not recommended
        // if (targetEnemy == null && _enemyExists)
        // {
        //     var tower = this.transform.parent.parent.gameObject;
        //     var controller = tower.GetComponent<TowerController>();
        //     if (controller.SelectedEnemy != null)
        //         targetEnemy = controller.SelectedEnemy;
        //     else _enemyExists = false;
        // }
        if (targetEnemy == null && EnemyExists) EnemyExists = false;
        
        
        MoveToEnemy();
        #region unused
        // else
        // {
        //     var controller = this.transform.parent.parent.GetComponent<TowerController>();
        //     if (controller.SelectedEnemy == null)
        //     {
        //         _freemove = true;
        //         StartCoroutine(DestroyTimer());
        //     }
        //     _targetEnemy = controller.SelectedEnemy;
        //
        // }
        #endregion
    }
    
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
    
    #region unused
    
    // private IEnumerator DestroyTimer()
    // {
    //     yield return new WaitForSeconds(5);
    //     _destroyed = true;
    // }

    // private void FreeMove()
    // {
    //     if (_destroyed)
    //         Destroy(this.gameObject);
    //     var ymod = 0f;
    //     float xmod = 0f;
    //     Vector2 farpos = ((Vector2)_parpos - _prevpos);
    //     //lazy to comment
    //     if (farpos.y >= 0)
    //         ymod = 4f;
    //     else ymod = -4f;
    //     
    //     if (farpos.x >= 0)
    //         xmod = 4f;
    //     else xmod = -4f;
    //     farpos.x = _prevpos.x * xmod;
    //     farpos.y = _prevpos.y * ymod;
    //     var dest = Vector2.MoveTowards(this.transform.position, farpos, speed * Time.deltaTime);
    //     this.transform.position = dest;
    // }
    #endregion
    protected virtual void MoveToEnemy()
    {
        var position = this.transform.position;
        if (EnemyExists) Prevpos = targetEnemy.transform.position;
        else
        {
            //todo move VectorEquals to somewhere else (definitely not creepController)
            if (CreepController.VectorEquals(position, Prevpos))
            {
                this.Destroyed = true;
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
}
