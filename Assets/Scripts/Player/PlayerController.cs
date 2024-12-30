using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IControllable
{
    public event Action OnShoot;

    private Rigidbody2D _rigidbody2D;

    [SerializeField]private float _speed = 100f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void LookAt(Vector2 direction)
    {
        var poin = Camera.main.ScreenToWorldPoint(direction);

        direction = (Vector2)poin - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Move(Vector2 direction)
    {
        _rigidbody2D.linearVelocity = direction* Time.deltaTime * _speed;
    }

    public void Shoot()
    {
        OnShoot?.Invoke();
    }
}
