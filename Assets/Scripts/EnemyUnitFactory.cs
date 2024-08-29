using UnityEngine;

[CreateAssetMenu(fileName = "EnemyUnitFactory", menuName = "ScriptableObjects/EnemyUnitFactory")]
public class EnemyUnitFactory : UnitFactory
{
    [SerializeField] private EnemyUnit enemyPrefab;

    public override BaseUnit CreateUnit(GameData data, Vector3 spawnPosition, Quaternion spawnRotation)
    {        
        float randomSpeed = Random.Range(data.minEnemySpeed, data.maxEnemySpeed);

        EnemyUnit enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        enemy.Initialize(data.enemyHealth, randomSpeed);

        return enemy;
    }

    public float GetRandomSpawnTime(GameData data)
    {
        return Random.Range(data.minEnemySpawnTime, data.maxEnemySpawnTime);
    }
}