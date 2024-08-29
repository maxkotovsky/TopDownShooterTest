using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PlayerUnit : BaseUnit
{
    public UnityEvent OnPlayerDeath;

    [SerializeField]
    private PlayerProjectile _projectile;

    [SerializeField]
    private GameObject _deathVFX;

    private float _attackCooldown;
    private float _attackRadius;
    private float _lastAttackTime;

    public void Initialize(GameData data)
    {
        unitData = data;
        base.Initialize(unitData.playerHealth);
        _attackCooldown = data.playerAttackCooldown;
        _attackRadius = data.playerAttackRadius;
    }

    private void ShootProjectile()
    {
        if (Time.time - _lastAttackTime >= _attackCooldown)
        {
            Transform nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && _projectile != null)
            {
                PlayerProjectile projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
                projectile.Initialize(unitData);
                projectile.SetTarget(nearestEnemy);
            }
            _lastAttackTime = Time.time;
        }
    }

    private void Update()
    {
        ShootProjectile();
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _attackRadius, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length > 0)
        {
            var nearestEnemy = enemiesInRange
                .Select(collider => collider.GetComponent<EnemyUnit>())
                .Where(enemy => enemy != null)
                .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
                .FirstOrDefault();

            return nearestEnemy != null ? nearestEnemy.transform : null;
        }

        return null;
    }

    public void OnEnemyHitFinish()
    {
        TakeDamage(1);
        Debug.Log($"Player health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        OnPlayerDeath?.Invoke();
        Instantiate(_deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.001f);
    }
}
