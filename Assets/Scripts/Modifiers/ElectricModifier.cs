using UnityEngine;

namespace Models
{
    public class ElectricModifier : Modifier
    {
        private float freezeScale;
        private float tickDamage;
        public ElectricModifier(float tickDamage, float freezeScale, int maxTicks, CreepController target) : base(true, false, maxTicks, target)
        {
            this.tickDamage = tickDamage;
            this.freezeScale = freezeScale;
        }

        public override void ApplyModifier()
        {
            TargetCreep.currentSpeed = TargetCreep.MaxSpeed - (TargetCreep.MaxSpeed * freezeScale);
            TargetCreep.TakeDamage(tickDamage);
        }

        public override void ModifierRemoved()
        {
            TargetCreep.RevertSpeed();
        }
    }
}