using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    protected GameData unitData;
    protected int currentHealth;

    public void Initialize(int health)
    {
        currentHealth = health;
        Debug.Log($"Unit initialized with health: {currentHealth}");
    }

    public int GetCurrentHealth() 
    {
        return currentHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public abstract void Die();
}