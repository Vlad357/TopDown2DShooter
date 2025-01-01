using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    
    public float shootingInterval = 2f; 
    public LayerMask obstacleLayer;

    public float changeTargetInterval = 1.5f; 

    private IControllable controllable;
    private EnemyMovement movement;

    private float lastShootTime = 0f;

    private void Awake()
    {
        controllable = GetComponent<IControllable>();
        movement = new EnemyMovement(transform, player, obstacleLayer, changeTargetInterval);
        movement.OnMove += controllable.Move;
    }

    private void Update()
    {
        if (CanHitPlayer())
        {
            movement.MoveTowardsPlayer();
        }
        else
        {
            movement.Move();
        }
        AimAtPlayer();
        TryShootAtPlayer();
    }

    private void AimAtPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void TryShootAtPlayer()
    {
        if (Time.time - lastShootTime >= shootingInterval)
        {
            if (CanHitPlayer() || CanHitPlayerWithRicochet())
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    private void Shoot()
    {
        controllable.Shoot();
    }

    private bool CanHitPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, Mathf.Infinity, obstacleLayer);
        return hit.collider != null && hit.collider.transform == player;
    }

    private bool CanHitPlayerWithRicochet()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, Mathf.Infinity, obstacleLayer);
        if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        {
            Vector2 reflectDirection = Vector2.Reflect((player.position - transform.position).normalized, hit.normal);
            RaycastHit2D secondHit = Physics2D.Raycast(hit.point, reflectDirection, Mathf.Infinity, obstacleLayer);
            return secondHit.collider != null && secondHit.collider.transform == player;
        }
        return false;
    }

    private Vector2 GetRandomDirectionAvoidingObstacles()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, randomDirection, 1f, obstacleLayer);
        return hit.collider == null ? randomDirection : -randomDirection;
    }
}
