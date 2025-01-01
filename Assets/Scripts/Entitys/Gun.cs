using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public event Action bulletIsCollisionEvent;

    public GameObject BulletPrefab;

    [SerializeField]private int layerMaskEnemy;

    [SerializeField]private float _spawnOffset = 0.75f;

    private void Start()
    {
        GetComponentInParent<IControllable>().OnShoot += Shoot;
    }

    private void Shoot()
    {
        Vector3 spawnPosition = transform.position + transform.up * _spawnOffset;
        var bulletObject = Instantiate(BulletPrefab, spawnPosition, transform.rotation);

        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.bulletIsCollision += OnBulletCollision;

        bullet.SetEnemyLayerMask(layerMaskEnemy);
        bullet.SetDirection(transform.up);
    }

    private void OnBulletCollision()
    {
        bulletIsCollisionEvent?.Invoke();
    }
}
