namespace Models
{
    public static class Enemies
    {
        public static readonly Enemy Light = new(EnemyType.Light, 10f, 1.2f, 2);
        public static readonly Enemy LightBoss = new(EnemyType.Light, 80f, 1.3f, 6, 1.3f);
        public static readonly Enemy Armed = new(EnemyType.Armed, 45f, 1f, 4);
        public static readonly Enemy ArmedBoss = new(EnemyType.Armed, 200f, .5f, 8, 1.3f);
        
    }
}