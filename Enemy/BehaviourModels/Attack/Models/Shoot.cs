using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class Shoot : Attack
    {
        [SerializeField] protected GameObject Trunk;
        [SerializeField] protected GameObject BulletPrefab;

        [SerializeField] protected float WaitTime;
        [SerializeField] protected float FlySpeed;
        [SerializeField] protected float DestroyTime;

        protected Enemy Enemy;
        protected Gun Gun;
        protected float Damage;
        protected float AttackSpeed;
        protected WallType Wall;

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

        protected virtual void Start()
        {
            Enemy = gameObject.GetComponent<Enemy>();
            Gun = Enemy.Gun;

            Damage = Gun.Damage;
            AttackSpeed = Gun.AttackSpeed;
        }

        public override void CanAttack()
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
            Player.Player enemy = hitObject.GetComponent<Player.Player>();

            if (enemy != null)
                TakeDamage(enemy, bullet, damage);
        }

        protected void TakeDamage(Player.Player player, GameObject bullet, float damage)
        {
            player.AcceptDamage(Damage);
            Destroy(bullet, damage);
        }
    }

}
