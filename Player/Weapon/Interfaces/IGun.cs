namespace Game.Player
{
    public interface IGun
    {
        public IGunType.Type GunType { get; }

        public uint ClipSize { get; }

        public float Damage { get; }

        public float AttackSpeed { get; }

        float Shoot();

        void Reload();
    }
}