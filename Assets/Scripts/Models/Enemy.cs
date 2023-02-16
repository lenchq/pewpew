using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Enemy
    {
        //public float Damage { get; private set; }
        public float Health { get; private set; }
        public int Cost { get; private set; }
        public float Size { get; private set; }
        public float Speed { get; private set; }
        public EnemyType Type { get; private set; }
        

        public Enemy(EnemyType type, float health, float speed, int cost, float size = 1f)
        {
            Type = type;
            Health = health;
            Speed = speed;
            Cost = cost;
            Size = size;
        }

        // public static List<Enemy> operator *(Enemy left, int right)
        // {
        //     var array = new List<Enemy>();
        //     for (int i = 0; i < right; i++)
        //     {
        //         array.Add(left);
        //     }
        //
        //     return array;
        // }

        public static Enemy[] Add(params Enemy[][] enemies)
        {
            List<Enemy[]> result = new List<Enemy[]>();
            foreach (Enemy[] enms in enemies)
            {
                result.Add(enms);
            }

            //to single strict array
            return result.SelectMany(item => item).ToArray();
        }
        public static Enemy[] operator *(Enemy left, int right)
        {
            var array = new List<Enemy>();
            for (int i = 0; i < right; i++)
            {
                array.Add(left);
            }

            return array.ToArray();
        }
    }

    public static class EnemyExtension
    {
        public static Enemy[] Shuffle(this Enemy[] enemies)
        {
            var list = new List<Enemy>(enemies);
            
            Random random = new Random();
            var shuffledList = list.OrderBy(x => random.Next());

            return shuffledList.ToArray();
        }
    }
}