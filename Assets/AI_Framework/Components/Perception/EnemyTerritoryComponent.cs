using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTerritoryComponent : MonoBehaviour
{
    [SerializeField] private EntityMediator enemy;

    public Vector2 SpawnPoint { get; private set; }

    [SerializeField] private float maxDistanceFromSpawn;

    private Vector2 positionToReach;

    private void Awake()
    {
        SpawnPoint = transform.position;
        positionToReach = GetRandomPointNearSpawn();
    }

    public bool IsOutsideTerritory(Vector2 pos)
    {
        return Vector2.Distance(pos, SpawnPoint) > maxDistanceFromSpawn;
    }

    public Vector2 GetRandomPointNearSpawn()
    {
        Vector2 randomOffset = Random.insideUnitCircle * maxDistanceFromSpawn;

        return SpawnPoint + randomOffset;
    }

    public void WanderAroundSpawnPoint(float speed)
    {
        enemy.E_Movement.MoveTo(positionToReach, speed, () =>
        {
            positionToReach = GetRandomPointNearSpawn();
        });
    }
}
