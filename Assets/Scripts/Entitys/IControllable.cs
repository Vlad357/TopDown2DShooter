using System;
using UnityEngine;

public interface IControllable
{
    public event Action OnShoot;

    public void Move(Vector2 direction);

    public void LookAt(Vector2 direction);

    public void LookAtPoint(Vector2 point);

    public void Shoot();
}
