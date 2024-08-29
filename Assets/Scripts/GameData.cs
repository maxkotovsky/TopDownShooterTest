using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Win Conditions Setup")]
    [Range(1, 50)]
    public int maxEnemyKillToWin;
    [Range(1, 50)]
    public int minEnemyKillToWin;

    [Header("Enemy Setup")]
    public int enemyHealth;

    [Header("Enemy Spawn Time")]
    [Range(0.3f, 3f)]
    public float minEnemySpawnTime;
    [Range(0.3f, 3f)]
    public float maxEnemySpawnTime;

    [Header("Enemy Speed")]
    [Range(1f, 10f)]
    public float minEnemySpeed;
    [Range(1f, 10f)]
    public float maxEnemySpeed;

    [Header("Player Setup")]
    public int playerHealth;

    [Header("Player Weapon Setup")]
    public float playerAttackRadius;
    public float playerAttackCooldown;
    public int playerAttackDamage;
    public float playerProjectileSpeed;
}
