using UnityEngine;

namespace Towers
{
    public class CannonTowerController : ReloadableTower
    {
        [SerializeField]
        private GameObject _bullet;
        [SerializeField]
        private Transform _bulletSpawn;
        [SerializeField]
        private float bulletSpeed;

        [SerializeField]
        private float bulletSize;
        
        [SerializeField]
        private float slowModifier;

        [SerializeField]
        private int modifierDuration;
        
        private AudioClip _shootSound;


        protected override void Start()
        {
            base.Start();
            _shootSound = Resources.Load<AudioClip>("Audio/Explosion_Shoot");
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
                Rotated = false;
                SelectEnemy();
            }
        
            FixAttackRadius();
        }

        protected override void PerformShootWithReload()
        {
            var bullet = (GameObject) Instantiate(_bullet,
                _bulletSpawn.position, _bulletSpawn.rotation);
            AudioPlayer.PlayOneShot(_shootSound);

            var script = bullet.GetComponent<ExplosiveBullet>();
            script.modifierFreezeScale = this.slowModifier;
            script.modifierTickDuration = this.modifierDuration;
            script.size = bulletSize;
            script.attackDamage = this.attackDamage;
            script.targetEnemy = SelectedEnemy;
            script.speed = this.bulletSpeed;
            script.audioSource = this.AudioPlayer;
        }


    }
}