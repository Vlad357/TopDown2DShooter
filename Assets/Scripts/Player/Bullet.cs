using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]private int _enemyLayerMask;

    [SerializeField]private float _force = 10f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction){
        _rigidbody2D.AddForce(direction * _force, ForceMode2D.Impulse);
    }

    public void SetEnemyLayerMask(int layerMask)
    {
        _enemyLayerMask = layerMask;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Field"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _enemyLayerMask)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(gameObject);
        }
    }
}