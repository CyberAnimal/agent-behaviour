using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class PistolShoot : Shoot
    {
        [SerializeField] private uint ShootAmount;

        private List<Bullet> _bullets;

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
        }

        private Bullet Shoot()
        {
            GameObject bulletObject = CreateBullet();
            Bullet bullet = new Bullet(bulletObject, Damage);

            StartCoroutine(Fly(bulletObject));

            return bullet;
        }

        private IEnumerator Fly(GameObject bullet)
        {
            Vector3 startPosition = bullet.transform.position;
            float step = FlySpeed * Time.deltaTime;
            Vector3 vectorStep = new Vector3(step, step, step);
            Vector3 destinationPoint = startPosition + vectorStep;
            float time = 0;

            if (time < 1)
            {
                bullet.transform.position = Vector3.Lerp(startPosition, destinationPoint, time);
                time += Time.deltaTime;

                yield return null;
            }

            else
            {
                StartCoroutine(Fly(bullet));
            }
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

                Check(bulletObject, damage);
            }
        }
    }

}
