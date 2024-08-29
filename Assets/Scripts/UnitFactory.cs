using UnityEngine;

public abstract class UnitFactory : ScriptableObject
{
    public abstract BaseUnit CreateUnit(GameData data, Vector3 spawnPosition, Quaternion spawnRotation);
}