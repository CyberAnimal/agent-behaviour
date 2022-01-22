namespace Game.Player
{
    public class Gun : IGun
    {
        private GunAdapter _adapter;
        private Clip _clip;

        public IGunType.Type GunType { get; private set; }
        public uint ClipSize { get; private set; }
        public float Damage { get; private set; }
        public float AttackSpeed { get; private set; }

        public Gun(IGunType.Type gunType)
        {
            GunType = gunType;
            SetParametrs(GunType);
            _clip = new Clip(ClipSize, Damage);
        }

        public void Reload()
        {
            _clip.TakeNew();
        }

        public float Shoot()
        {
            _clip.ReductionClip();
            return Damage;
        }

        private void SetParametrs(IGunType.Type gunType)
        {
            _adapter = GunAdapter.CreateAdapter(gunType);

            ClipSize = _adapter.ClipSize;
            Damage = _adapter.Damage;
            AttackSpeed = _adapter.AttackSpeed;
        }
    }
}