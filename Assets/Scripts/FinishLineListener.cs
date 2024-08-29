using UnityEngine;
using UnityEngine.Events;

public class FinishLineListener : MonoBehaviour
{
    public UnityEvent OnEnemyHit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var enemyUnit = collider.GetComponent<EnemyUnit>();
        if (enemyUnit != null)
        {
            OnEnemyHit?.Invoke();
            enemyUnit.DeathOnHit();
        }
    }
}