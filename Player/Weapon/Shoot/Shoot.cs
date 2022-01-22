using UnityEngine;

namespace Game.Player
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] protected KeyboardInput Input;
        [SerializeField] protected GameObject Trunk;
        [SerializeField] protected GameObject BulletPrefab;

        [SerializeField] protected float WaitTime;
        [SerializeField] protected float FlySpeed;
        [SerializeField] protected float DestroyTime;

        protected Player Player;
        protected float Damage;
        protected float AttackSpeed;
        protected WallType Wall;
        protected IGun Gun;

        protected enum ShootState
        {
            Shoot,
            Wait,
            Free
        }

        protected ShootState State = ShootState.Free;

        protected class Bullet
        {
            public GameObject BulletObject;
            public float Damage;

            public Bullet(GameObject bulletObject, float damage)
            {
                BulletObject = bulletObject;
                Damage = damage;
            }
        }

        public virtual IGunType.Type GunType { get; }

        protected virtual void Start()
        {
            Player = gameObject.GetComponent<Player>();
            Gun = Player.Weapon.CurrentGun;

            Damage = Gun.Damage;
            AttackSpeed = Gun.AttackSpeed;

            if (Input == null)
                Input = Player.GetComponent<KeyboardInput>();
        }

        protected virtual void OnEnable()
        {
            Input.Shoot += ChangeState;
        }

        protected virtual void OnDisable()
        {
            Input.Shoot -= ChangeState;
        }

        protected void ChangeState()
        {
            if (State == ShootState.Free)
                State = ShootState.Shoot;
        }

        protected bool IsShoot()
        {
            if (State == ShootState.Shoot)
                return true;

            return false;
        }

        protected bool IsWait()
        {
            if (State == ShootState.Shoot)
                return true;

            return false;
        }
        protected void Check(GameObject bullet, float damage)
        {
            Vector3 startPosition = bullet.transform.position;
            Vector3 endPosition = bullet.transform.forward;
            float radius = bullet.transform.lossyScale.y / 2;

            RaycastHit hit;
            Ray ray = new Ray(startPosition, endPosition);

            bool isCollision = Physics.SphereCast(ray, radius, out hit);

            if (isCollision)
            {
                if (IsWall(hit))
                {
                    Destroy(bullet, DestroyTime);
                }

                CheckEnemy(hit, bullet, damage);
            }
        }

        protected bool IsWall(RaycastHit hit)
        {
            GameObject hitObject = hit.collider.gameObject;
            ObjectType type = hitObject.GetComponent<ObjectType>();

            if (type == ObjectType.Wall)
                return true;

            return false;
        }

        protected void CheckEnemy(RaycastHit hit, GameObject bullet, float damage)
        {
            GameObject hitObject = hit.collider.gameObject;
            Enemy.Enemy enemy = hitObject.GetComponent<Enemy.Enemy>();

            if (enemy != null)
                TakeDamage(enemy, bullet, damage);
        }

        protected void TakeDamage(Enemy.Enemy enemy, GameObject bullet, float damage)
        {
            enemy.ApplyDamage(Damage);
            Destroy(bullet, damage);
        }
    }
}