using System;
using UnityEngine;

public interface IControllable
{
    public event Action OnShoot;

    public void Move(Vector2 direction);

    public void LookAt(Vector2 direction);

    public void Shoot();
}
