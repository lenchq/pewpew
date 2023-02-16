namespace Models
{
    public class ExplodeSlowModifier : Modifier
    {
        protected float freezeScale;
        public ExplodeSlowModifier(int ticksDuration, CreepController targetCreep, float freezeScale)
            : base(false, false, ticksDuration, targetCreep)
        {
            this.freezeScale = freezeScale;
        }

        public override void ApplyModifier()
        {
            TargetCreep.currentSpeed = TargetCreep.MaxSpeed - (TargetCreep.MaxSpeed * freezeScale);
        }
    }
}