using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ObstacleType
{
    singleLane, doubleLane, tripleLane

}
[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacle")]
public class Obstacle : ScriptableObject
{
    public ObstacleType obstacleType;
    public GameObject prefab;
    public float laneSpawnDelay;
    public float chanceToSpawn;

}
