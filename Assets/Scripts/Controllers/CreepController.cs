using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
public class CreepController : MonoBehaviour
{
    // public class Infuencer
    // {
    //     //public delegate bool? Influence(ref CreepController cc);
    //
    //     public virtual void PerformInfluence(ref CreepController cc)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public virtual void CallInfluence()
    //     {
    //         isActive = true;
    //         _wasClb = true;
    //     }
    //
    //     public void HandleTick()
    //     {
    //         if (!_wasClb && _ticks < _maxTicks)
    //         {
    //             _ticks++;
    //         }
    //         else if (!_wasClb && _ticks >= _maxTicks)
    //         {
    //             _ticks = 0;
    //             isActive = false;
    //         }
    //
    //         _wasClb = false;
    //     }
    //     
    //     protected bool _wasClb = false;
    //     public bool isActive = false;
    //     protected int _ticks;
    //     protected int _maxTicks;
    //
    //     public Infuencer(int maxTicks = 10)
    //     {
    //         _maxTicks = maxTicks;
    //     }
    //
    //     public event Action OnCall;
    // }
    
    private Rigidbody2D _rigidbody;

    private Collider2D _collider;

    private Transform _position;

    private int _currentPointIndex = 0;
    private Transform _currentPoint;
    private List<Transform> _pathPoints;
    private CreepPathController _currentPathController;
    
    //electric private
    // private int _electricTicks = 0;
    // private bool _wasElectricClb = false;
    // private float _electricSpeedScale;
    // private float _electricTickDamage;
    
    [NonSerialized]
    public float MaxHp;
    [NonSerialized]
    public float MaxSpeed;
    
    protected List<Modifier> Modifiers = new List<Modifier>();
    
    private Transform _healthBar;

    protected CreepController Instance;

    #region  public

    [SerializeField]
    private bool isElectric = false;
    
    [SerializeField]
    public int creepCost = 5;

    [SerializeField]
    public int creepLevel = 1;

    [SerializeField]
    private int maxElectricTicks = 10;
    
    [SerializeField]
    public float health = 30;

    [SerializeField]
    public CreepPathController creepPathController;

    [SerializeField]
    public float currentSpeed = 1f;


    
    [SerializeField]
    private bool movingEnabled = true;

    private List<GameObject> _electricDealers;

    // protected List<Infuencer> Infuencers;
    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _position = GetComponent<Transform>();
        _healthBar = GetComponentsInChildren<Transform>()[3];
        MaxHp = health;
        MaxSpeed = currentSpeed;
        _electricDealers = new List<GameObject>();
        if (movingEnabled)
        {
            if (creepPathController is null) {
                Debug.Log($"Creep couldn't find the creep path controller. Moving disabled");
                movingEnabled = false;
            }
            else
            {
                _pathPoints = creepPathController.GetPoints();
                _currentPathController = creepPathController;
                //start from first pos
                _currentPoint = _pathPoints.First();
            }
        }

        Instance = this;

    }

    // Update is called once per frame
    // public void Electric(float electricSpeedScale, float electricTickDamage, ElectricTowerController tower)
    // {
    //     
    //     
    //     _wasElectricClb = true;
    //     isElectric = true;
    //     _electricTicks = 0;
    //     _electricTickDamage = electricTickDamage;
    //     _electricSpeedScale = electricSpeedScale;
    //     if (!_electricDealers.Contains(tower.gameObject))
    //     {
    //         _electricDealers.Add(tower.gameObject);
    //     }
    // }

    public void ApplyModifier(Modifier mod)
    {
        var mods = Modifiers.Where((appliedMod) => appliedMod.GetType() == mod.GetType()).ToList();
        if (mods.Any() && !mod.Stackable)
        {
            if (mod.Refreshable)
            {
                var appliedMod = mods.First();
                appliedMod.Refresh();
            }
            else return;
            return;
        }
        //else  stack modifiers (in some way like arrays maybe idk)
        Modifiers.Add(mod);
        mod.OnEnd += () =>
        {
            Modifiers.Remove(mod);
            mod.ModifierRemoved();
        };
    }
    
    private void FixedUpdate()
    {
        if (Modifiers.Count > 0)
        {
            try
            {
                foreach (var mod in Modifiers)
                {
                    mod.Tick();
                    if (mod.IsActive)
                        mod.ApplyModifier();
                }
            }
            catch
            {
                // ignored
            }
        }
        
        
        
        
        // ElectricInfluence();

        foreach (var towerObject in _electricDealers)
        {
            var tower = towerObject.GetComponent<TowerController>();

            if (tower.SelectedEnemy != Instance.gameObject)
            {
                _electricDealers.Remove(towerObject);
                break;
            }
        }

        // foreach (var inf in Infuencers)
        // {
        //     inf.HandleTick();
        //     if (inf.isActive)
        //     {
        //         inf.PerformInfluence(ref Instance);
        //     }
        //     
        // }

        HandleMove();
    }
    
    #region electric old
    // private void ElectricInfluence()
    // {
    //     if (isElectric)
    //     {
    //         //Debug.Log("On Electric!!");
    //         currentSpeed = MaxSpeed - (MaxSpeed * _electricSpeedScale);
    //         //Debug.Log(_electricDealers.Count);
    //         TakeDamage(_electricTickDamage * _electricDealers.Count);
    //     }
    //     else
    //     {
    //         currentSpeed = MaxSpeed;
    //     }
    // }
    #endregion

    void Update()
    {
        FixPathController();
        UpdateHp();
        CheckNextPoint();
    }

    private void FixPathController()
    {
        if (_currentPathController is null)
        {
            if (creepPathController is not null)
                _currentPathController = creepPathController;
            else
            {
                movingEnabled = false;
                return;
            }
        }
        else
        {
            if (_pathPoints is null || _currentPoint is null)
            {
                _pathPoints = creepPathController.GetPoints();
                _currentPoint = _pathPoints.First();
            }
        }
        if (_currentPathController != creepPathController)
        {
            _pathPoints = creepPathController.GetPoints();
            //start from first pos
            _currentPoint = _pathPoints.First();
            _currentPathController = creepPathController;
        }
    }

    private void CheckNextPoint()
    {
        if (_pathPoints is null) FixPathController();
        
        if (movingEnabled && VectorEquals(_position.position, _currentPoint.position))
        {
            if (_currentPointIndex + 1 > _pathPoints.Count - 1)
            {
                //_movingEnabled = false; // stop at last point
                _currentPointIndex = 0; // or repeat
            }
            else _currentPointIndex++;

            _currentPoint = _pathPoints[_currentPointIndex];
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"Collide {other.name}");
    }

    private void OnParticleTrigger()
    {
        Debug.Log($"Collide");
    }



    private void UpdateHp()
    {
        var percent = health / MaxHp;
        float hpbarX = 0;
        if (percent < 1f)
            hpbarX = -((1 - percent) / 2);
        _healthBar.localPosition = new Vector3(hpbarX, 0, -.01f);
        _healthBar.localScale = new Vector3(percent, 1, 1);
    }

    void HandleMove()
    {
        
        if (_currentPoint is null || !movingEnabled) return;
        var direction = Vector3.MoveTowards(_position.position, _currentPoint.position,
            currentSpeed * Time.deltaTime);
        this.transform.position = direction;
    }
    public static bool VectorEquals(Vector2 a, Vector2 b)
    {
        a.x = (float)Math.Round(a.x, 2);
        a.y = (float)Math.Round(a.y, 2);
        
        b.x = (float)Math.Round(b.x, 2);
        b.y = (float)Math.Round(b.y, 2);

        var x = Math.Abs(a.x - b.x);
        var y = Math.Abs(a.y - b.y);

        //return a.x == b.x && a.y == b.y;
        return x < .3f && y < .3f;
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            this.Die();
        }
    }

    // private int GetCreepCost()
    // {
    //     
    // }

    public void OnDie(bool giveMoney = true)
    {
      
        //GameEvents.current.AddMoney(this.creepCost);
        if (giveMoney)
            GlobalVars.current.AddMoney(this.creepCost);
        GameEvents.current.CreepDied();
    }

    public void Die(bool giveMoney = true)
    {
        //todo particles
        OnDie(giveMoney);
        Destroy(this.gameObject);
    }
    public void RevertSpeed()
    {
        this.currentSpeed = MaxSpeed;
    }
}
