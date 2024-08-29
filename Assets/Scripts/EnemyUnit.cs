using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnemyUnit : BaseUnit
{
    public UnityEvent OnEnemyDeathOnKill;

    private float speed;
    [SerializeField]
    private GameObject _impactHitVFX;
    [SerializeField]
    private GameObject _impactDeathVFX;
    [SerializeField]
    private GameObject _spawnVFX;

    public void Initialize(int randomHealth, float randomSpeed)
    {
        this.speed = randomSpeed;
        base.Initialize(randomHealth);
    }

    public void OnEnable()
    {
        Instantiate(_spawnVFX, transform.position, Quaternion.identity);
    }

    public void Move()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    public void DeathOnHit()
    {
        Collider2D enemyCollider = GetComponent<Collider2D>();
        enemyCollider.enabled = false;
        transform.DOShakeScale(0.3f, 0.4f, 15, 90, false)
                 .OnComplete(() =>
                  {
                      Instantiate(_impactHitVFX, transform.position, transform.rotation);
                      DOTween.Kill(transform);
                      Die();
                  });
        
    }

    public void OnProjectileHit(int damage)
    {
        TakeDamage(damage);
        Debug.Log($"Enemy health: {currentHealth}");

        if (currentHealth <= 0)
        {
            DeathOnKill();
        }
    }

    public void DeathOnKill()
    {
        OnEnemyDeathOnKill?.Invoke();
        Instantiate(_impactDeathVFX, transform.position, transform.rotation);
        Die();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Move();
    }

}