using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Wave
    {
        public Enemy[] Enemies { get; private set; }

        public List<Enemy> EnemiesList => this.Enemies.ToList();

        
        public TimeSpan WaveTimeout { get; private set; }
        public int WaveTimeoutSeconds
        {
            get => WaveTimeout.Seconds;
        }

        public Wave(Enemy[] enemies, TimeSpan timeout)
        {
            Enemies = enemies;
            WaveTimeout = timeout;
        }

        // private Enemy[] ParseEnemies(EnemyType[] enemies)
        // {
        //     Enemy[] return_ens = new Enemy[enemies.Length];
        //     var i = 0;
        //     foreach (var enemy in enemies)
        //     {
        //         if (enemy == EnemyType.Armed) return_ens[i] = Models.Enemies.Armed;
        //
        //     }
        // }
    }
}