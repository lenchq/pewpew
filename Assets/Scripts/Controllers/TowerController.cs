using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class TowerController : MonoBehaviour
{
    public enum AttackBehaviour
    {
        TowerNearestEnemy,
        //TowerFarthestEnemy,
        BaseNearestEnemy,
        BaseFarthestEnemy,
        StrongestEnemy,
        LowHpEnemy,
        Random,
    }

    
    [FormerlySerializedAs("AttackRadius")]
    
    [Header("Stats")]
    [SerializeField]
    [Range(1f, 8f)]
    public float attackRadius = 3f;

    [SerializeField]
    public AttackBehaviour attackBehaviour = AttackBehaviour.TowerNearestEnemy;
    
    [FormerlySerializedAs("AttackDamage")]
    [SerializeField]
    public float attackDamage = 8;
    
    [FormerlySerializedAs("_rotationSpeed")]
    [SerializeField]
    public float rotationSpeed = 3f;

    [SerializeField]
    public string Name = "Вышка";

    [Header("Upgrade")]
    [SerializeField]
    public int level = 1;
    [SerializeField]
    public int initialUpgradeCost = 20;
    [SerializeField]
    public float attackDamageUpgradeScale = .4f;
    [SerializeField]
    public float attackRadiusUpgradeScale = .2f;

    [Header("Buy")]
    public int towerCost = 15;

    public GameObject SelectedEnemy { get; protected set; }
    
    protected AudioSource AudioPlayer;
    protected List<AudioClip> ShotSounds;
    protected CircleCollider2D CircleCollider;
    protected List<Collider2D> Colliders;
    protected bool Rotated;


    private GameObject _playerBase;
    protected virtual float RotationTolerance => 15;

//    protected string TowerUpgradeText;
    protected event Func<string> OnAdditionalUpgradeTextFormat;
    protected event Action OnUpgrade;




    protected virtual void Start()
    {
        
        CircleCollider = GetComponentInChildren<CircleCollider2D>();
        CircleCollider.radius = this.attackRadius;
        Colliders = new List<Collider2D>();
        _playerBase = GameObject.Find("PlayerBase");
        AudioPlayer = this.GetComponent<AudioSource>();
        //_shotSounds = new List<AudioClip>();
        
        ShotSounds = Resources.LoadAll<AudioClip>("Audio/Shoot/").ToList();
    }
    public virtual void TriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!Colliders.Contains(other))
            {
                Colliders.Add(other);
            }
        }
    }

    public virtual void TriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Colliders.Contains(other))
            {
                Colliders.Remove(other);

                if (other.gameObject == SelectedEnemy)
                {
                    SelectedEnemy = null;
                }
            }
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
        if (SelectedEnemy != null)
            Gizmos.DrawLine(this.transform.position, SelectedEnemy.transform.position);
    }

    protected virtual void Update()
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
    protected virtual void FixedUpdate()
    {
        HandleRotation();
    }

    protected void FixAttackRadius()
    {
        if (Math.Abs(attackRadius - CircleCollider.radius) > 0)
        {
            CircleCollider.radius = attackRadius;
        }
    }

    protected virtual void HandleRotation()
    {
        if (SelectedEnemy != null)
        {
            //get gun's transform
            var gun = GetComponentsInChildren<Transform>()[1];
            var position = gun.position;

            Vector3 dir = position - SelectedEnemy.transform.position;
            // idk why +90deg it just works
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg )+ 90; // Assuming you want degrees not radians?
            float currentAngle;
            
            if(gun.eulerAngles.z >= 265f)
            {
                currentAngle = gun.eulerAngles.z - 360f;
            }
            else
            {
                currentAngle = gun.eulerAngles.z;
            }
            
            if (Math.Abs(currentAngle - angle) < RotationTolerance)
            {
                Rotated = true;
            }
            else Rotated = false;
            
            var q = Quaternion.Euler(0f, 0f, angle);
            var direction = Quaternion.Slerp(gun.rotation, q, rotationSpeed * Time.deltaTime);

            gun.SetPositionAndRotation(position, direction);
        }
        
    }

    #region Enemy Selection

    public void SelectEnemy()
    {
        try
        {
            switch (attackBehaviour)
            {
                case AttackBehaviour.TowerNearestEnemy:
                    FindNearestEnemy();
                    break;
                //case AttackBehaviour.TowerFarthestEnemy:
                //FindFarthestEnemy();
                // break;
                case AttackBehaviour.StrongestEnemy:
                    FindStrongestEnemy();
                    break;
                case AttackBehaviour.LowHpEnemy:
                    //FindLowHpEnemy();
                    break;
                case AttackBehaviour.Random:
                    FindRandomEnemy();
                    break;
                case AttackBehaviour.BaseNearestEnemy:
                    FindBaseNearestEnemy();
                    break;
                case AttackBehaviour.BaseFarthestEnemy:
                    FindBaseFarthestEnemy();
                    break;
            }
        }
        catch
        {
            Colliders = new List<Collider2D>();
        }
    }

    private void FindBaseFarthestEnemy()
    {
        var position = _playerBase.transform.position;
        if (Colliders.Count == 0)
        {
            SelectedEnemy = null;
        }
        else if (Colliders.Count == 1)
        {
            SelectedEnemy = Colliders[0].gameObject;
        }
        else if (Colliders.Count > 1)
        {
            var distance = float.MinValue;
            Collider2D farthest = new Collider2D();
            for (var i = 0; i < Colliders.Count; i++)
            {
                var colDist = Vector3.Distance(position, Colliders[i].gameObject.transform.position);
                if (colDist > distance)
                {
                    distance = colDist;
                    farthest = Colliders[i];
                }
            }
            SelectedEnemy = farthest.gameObject;
        }
    }

    protected void FindStrongestEnemy()
    {
        if (Colliders.Count == 0)
        {
            SelectedEnemy = null;
        }
        else if (Colliders.Count == 1)
        {
            SelectedEnemy = Colliders[0].gameObject;
        }
        else if (Colliders.Count > 1)
        {
            var maxHp = 0f;
            var closest = new Collider2D();
            for (var i = 0; i < Colliders.Count; i++)
            {
                //var colDist = Vector3.Distance(position, _colliders[i].gameObject.transform.position);
                var creep = Colliders[i].gameObject.GetComponent<CreepController>();
                var currHp = creep.health;
                if (currHp > maxHp)
                {
                    maxHp = currHp;
                    closest = Colliders[i];
                }
            }
            SelectedEnemy = closest.gameObject;
        }
    }

    protected void FindRandomEnemy()
    {
        
        if (Colliders.Count == 0)
        {
            SelectedEnemy = null;
        }
        else if (Colliders.Count == 1)
        {
            SelectedEnemy = Colliders[0].gameObject;
        }
        else if (Colliders.Count > 1)
        {
            var rnd = new Random();
            SelectedEnemy = Colliders[rnd.Next(Colliders.Count-1)].gameObject;
        }
        
    }

    protected void FindBaseNearestEnemy()
    {
        var position = _playerBase.transform.position;
        if (Colliders.Count == 0)
        {
            SelectedEnemy = null;
        }
        else if (Colliders.Count == 1)
        {
            SelectedEnemy = Colliders[0].gameObject;
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
            SelectedEnemy = closest.gameObject;
        }
    }

    public virtual int GetUpgradeCost()
    {
        return (int)Math.Round(initialUpgradeCost * (float)Math.Log(level, 1.8f) + initialUpgradeCost);
    }

    protected void FindNearestEnemy()
    {
        var position = this.transform.position;
        if (Colliders.Count == 0)
        {
            SelectedEnemy = null;
        }
        else if (Colliders.Count == 1)
        {
            SelectedEnemy = Colliders[0].gameObject;
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
            SelectedEnemy = closest.gameObject;
        }
    }
    #endregion
    protected virtual void PerformShoot()
    {
        Debug.LogWarning(@$"{this.gameObject.name} trying to shoot, but no PerformShoot method here.
             Try to override PerformShoot() method");
    }

    protected AudioClip GetRandomShootSound()
    {
        Random rnd = new Random();
        return ShotSounds[rnd.Next(ShotSounds.Count - 1)];
    }

    public void SetAttackBehavior(AttackBehaviour newBehavior)
    {
        attackBehaviour = newBehavior;
        SelectedEnemy = null;
        SelectEnemy();
    }

    public string GetUpgradeTextFormat()
    {
        var towerUpgradeText =
            Name+", {0} + {1} уровень\n" +
            "Урон: {2:F1} + {3:F1}\n" +
            "Дальность: {4:F1} + {5:F1}\n";
        var upgradeText = string.Format(towerUpgradeText, level, 1,
            attackDamage, attackDamage * attackDamageUpgradeScale,
            attackRadius, attackRadius * attackRadiusUpgradeScale
        );

        string additionals = "";
        foreach (Func<string> getAdditional in OnAdditionalUpgradeTextFormat?.GetInvocationList()!)
        {
            var additional = getAdditional();
            additionals += additional;
        }

        upgradeText += additionals;
        
        return upgradeText;
    }

    public void Upgrade()
    {
        this.level += 1;
        this.attackDamage += this.attackDamage * this.attackDamageUpgradeScale;
        this.attackRadius += this.attackRadius * this.attackRadiusUpgradeScale;
        OnUpgrade?.Invoke();
    }
}
