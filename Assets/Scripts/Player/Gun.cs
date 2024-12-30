using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;

    [SerializeField]private int layerMaskBullet;
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
        bulletObject.layer = layerMaskBullet;

        var bullet = bulletObject.GetComponent<Bullet>();

        bullet.SetEnemyLayerMask(layerMaskEnemy);
        bullet.SetDirection(transform.up);
    }
}
