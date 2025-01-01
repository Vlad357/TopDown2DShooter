using System;
using UnityEngine;

public class EnemyMovement
{
    public event Action<Vector2> OnMove;

    private Transform enemy; 
    private Transform player; 
    private LayerMask obstacleLayer; 
    private Vector2 targetPosition; 
    private float changeTargetInterval; 
    private float lastChangeTime; 

    private float minX = -8.5f, maxX = 8.5f, minY = -5f, maxY = 5f;

    public EnemyMovement(Transform enemy, Transform player, LayerMask obstacleLayer, float changeTargetInterval)
    {
        this.enemy = enemy;
        this.player = player;
        this.obstacleLayer = obstacleLayer;
        this.changeTargetInterval = changeTargetInterval;
        lastChangeTime = Time.time;
        SetRandomTarget(); 
    }

    public void Move()
    {
        Vector2 currentPosition = enemy.position;
        Vector2 direction = (targetPosition - currentPosition).normalized;

        
        if (Physics2D.Raycast(currentPosition, direction, 0.5f, obstacleLayer))
        {
            
            SetRandomTarget();
        }
        else
        {
            OnMove?.Invoke(direction);
        }

        if (Time.time - lastChangeTime > changeTargetInterval)
        {
            SetRandomTarget();
        }
    }

    private void SetRandomTarget()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * 5f;
        targetPosition = (Vector2)enemy.position + randomDirection;

        
        targetPosition = ClampToFieldBounds(targetPosition);

        lastChangeTime = Time.time;
    }

    private Vector2 ClampToFieldBounds(Vector2 position)
    {
        return new Vector2(Mathf.Clamp(position.x, minX, maxX), Mathf.Clamp(position.y, minY, maxY));
    }

    public void MoveTowardsPlayer()
    {
        targetPosition = player.position;
    }
}
