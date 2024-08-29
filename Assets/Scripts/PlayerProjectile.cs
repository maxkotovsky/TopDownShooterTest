using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private GameObject _hitVFX;
    [SerializeField]
    private float _delaySelfDestruct = 3f;

    protected GameData projectileData;

    private float _speed;
    private int _damage;
    private Vector3 _direction;
    private Transform _targetEnemy;

    public void Initialize(GameData data)
    {
        projectileData = data;
        _speed = projectileData.playerProjectileSpeed;
        _damage = projectileData.playerAttackDamage;
    }

    private void OnEnable()
    {
        Destroy(gameObject, _delaySelfDestruct);
    }

    public void SetTarget(Transform enemy)
    {
        _targetEnemy = enemy;
    }

    private void Homing()
    {
        Vector3 direction = (_targetEnemy.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        transform.SetPositionAndRotation(Vector2.MoveTowards(transform.position, _targetEnemy.position, _speed * Time.deltaTime), targetRotation);
    }

    private void Update()
    {
        if (_targetEnemy != null)
        {
            Homing();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_hitVFX != null)
        {
            Instantiate(_hitVFX, transform.position, transform.rotation);
        }

        var enemyUnit = collider.GetComponent<EnemyUnit>();
        if (enemyUnit != null)
        {
            enemyUnit.OnProjectileHit(_damage);
        }

        Destroy(gameObject);
    }
}