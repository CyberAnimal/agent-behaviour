using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class BallisticShoot : Shoot
    {
        [SerializeField] private uint Force;

        private List<Bullet> _bullets;

        public override IGunType.Type GunType => IGunType.Type.Shotgun;

        protected override void Start()
        {
            base.Start();

            _bullets = new List<Bullet>();
        }

        private void Update()
        {
            if (IsShoot())
            {
                _bullets.Add(Shoot());
                Waiting();
            }

            if (_bullets != null)
                UseBulletPool();

            if (IsReload())
                Gun.Reload();
        }

        private Bullet Shoot()
        {
            GameObject bulletObject = CreateBullet();
            float damage = Player.CanShoot();
            Bullet bullet = new Bullet(bulletObject, damage);

            return bullet;
        }

        private void Fly(GameObject bullet)
        {
            Quaternion rotation = bullet.transform.rotation;
            Vector3 direction = new Vector3(rotation.x, rotation.y, rotation.z + 45);
            direction = direction * Force;

            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            rigidbody.AddForce(direction, ForceMode.VelocityChange);
        }

        private GameObject CreateBullet()
        {
            Vector3 startPosition = Trunk.transform.position;
            Quaternion rotation = Trunk.transform.rotation.normalized;
            GameObject bullet = Instantiate(BulletPrefab, startPosition, rotation) as GameObject;

            return bullet;
        }

        private bool IsReload()
        {
            if (_bullets.Count == Gun.ClipSize)
                return true;

            return false;
        }

        private void Waiting()
        {
            State = ShootState.Wait;

            StartCoroutine(CheckState());
        }

        private IEnumerator CheckState()
        {
            float timeCount = 0.0f;

            while (timeCount < WaitTime)
            {
                timeCount += Time.deltaTime;

                yield return null;
            }

            yield return State = ShootState.Free;
        }

        private void UseBulletPool()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                Bullet bullet = _bullets[i];
                GameObject bulletObject = bullet.BulletObject;
                float damage = bullet.Damage;

                Fly(bulletObject);
                Check(bulletObject, damage);
            }
        }
    }
}