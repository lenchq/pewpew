using System;

public abstract class Modifier
{
    public int TicksDuration { get; protected set; }
    public int CurrentTicks { get; protected set; }
    public bool IsActive { get; protected set; } = true;

    public readonly bool Stackable;
    public readonly bool Refreshable;
    public event Action OnEnd;
    protected CreepController TargetCreep;

    protected Modifier(bool refreshable, bool stackable, int ticksDuration, CreepController targetCreep)
    {
        this.TargetCreep = targetCreep;
        TicksDuration = ticksDuration;
        Refreshable = refreshable;
        Stackable = stackable;
    }

    public void Refresh()
    {
        this.CurrentTicks = 0;
    }

    public void Tick()
    {
        if (CurrentTicks >= TicksDuration)
        {
            End();
            return;
        }
        CurrentTicks++;
    }

    public virtual void ModifierRemoved() { TargetCreep.RevertSpeed(); }


    public virtual void ApplyModifier()
    {
        throw new NotImplementedException();
    }

    public void End()
    {
        this.IsActive = false;
        OnEnd?.Invoke();
    }
    
}