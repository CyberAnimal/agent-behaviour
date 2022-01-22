using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class AutomaticShoot : Shoot
    {
        [SerializeField] private uint ShootAmount;

        private List<List<Bullet>> _allBullets;
        private bool _isTakeReload;

        public override IGunType.Type GunType => IGunType.Type.Automatic;

        protected override void Start()
        {
            base.Start();

            _allBullets = new List<List<Bullet>>();
            _isTakeReload = true;
        }

        private void Update()
        {
            if (IsShoot() && _isTakeReload)
                _allBullets.Add(Shoot());

            if (_allBullets != null)
                UseBulletPool();

            if (IsReload())
                Reloading();
        }

        private List<Bullet> Shoot()
        {
            List<Bullet> bullets = new List<Bullet>();
            _isTakeReload = false;

            StartCoroutine(ShootSequence(bullets));

            return bullets;
        }

        private IEnumerator ShootSequence(List<Bullet> bullets)
        {
            while (bullets.Count < ShootAmount)
            {
                GameObject bulletObject = CreateBullet();
                float damage = Player.CanShoot();

                Bullet bullet = new Bullet(bulletObject, damage);
                bullets.Add(bullet);


                StartCoroutine(Fly(bulletObject));

                yield return new WaitWhile(() => IsShoot());
            }

            yield return State = ShootState.Wait;
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
            for (int i = 0; i < _allBullets.Count; i++)
            {
                List<Bullet> bullets = _allBullets[i];

                if (bullets.Count >= ShootAmount)
                    return true;
            }

            return false;
        }

        private void Reloading()
        {
            State = ShootState.Wait;
            Gun.Reload();

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

            _isTakeReload = true;
            State = ShootState.Free;

            yield return null;
        }

        private void UseBulletPool()
        {
            for (int i = 0; i < _allBullets.Count; i++)
            {
                List<Bullet> bullets = _allBullets[i];

                for (int j = 0; j < bullets.Count; j++)
                {
                    Bullet bullet = bullets[j];
                    GameObject bulletObject = bullet.BulletObject;
                    float damage = bullet.Damage;

                    Check(bulletObject, damage);
                }
            }
        }
    }
}
