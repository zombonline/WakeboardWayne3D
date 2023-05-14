using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Obstacle[] prefabs, looseRamps, looseRailings, looseObstacles;
    [SerializeField] Track[] tracks;
    [SerializeField] float timeBetweenSpawns;

    [SerializeField] float prefabChance, looseRampChance, looseRailingChance, looseObstacleChance;
    float timer;
    public float timerEffector;

    private void Awake()
    {
        timer = timeBetweenSpawns;
    }

    private void Update()
    {
        timer -= Time.deltaTime * timerEffector;
        if(timer <= 0)
        {
            var randomNumber = Random.Range(0, 100f);
            if (randomNumber < prefabChance)
            {
                SpawnObstacle(prefabs);
            }
            else if(randomNumber < prefabChance + looseRampChance)
            {
                SpawnObstacle(looseRamps);
            }
            else if(randomNumber < prefabChance + looseRampChance + looseRailingChance)
            {
                SpawnObstacle(looseRailings);
            }
            else
            {
                SpawnObstacle(looseObstacles);
            }
        }
    }
    private void SpawnObstacle(Obstacle[] obstacleList)
    {

        var randomObstacle = obstacleList[Random.Range(0, obstacleList.Length)];

        bool unableToSpawn = false;
        var randomTrackID = 0;
        switch (randomObstacle.obstacleType)
        {
            case ObstacleType.singleLane:
                randomTrackID = Random.Range(0, tracks.Length);
                if (tracks[randomTrackID].spawnDisabled)
                {
                    unableToSpawn = true;
                }
                else
                {
                }
                    break;

            case ObstacleType.doubleLane:
                randomTrackID = Random.Range(0, tracks.Length-1);
                if (tracks[randomTrackID].spawnDisabled || tracks[randomTrackID + 1].spawnDisabled)
                {
                    unableToSpawn = true;
                }
                else
                {
                }
                    break;
            case ObstacleType.tripleLane:
                randomTrackID = 1;
                if (tracks[randomTrackID].spawnDisabled || tracks[randomTrackID + 1].spawnDisabled || tracks[randomTrackID-1].spawnDisabled)
                {
                    unableToSpawn = true;
                }
                else
                {
                }
                break;

        }
        if(unableToSpawn)
        {
            timer = timeBetweenSpawns / 1.5f;
            return;
        }
        timer = timeBetweenSpawns;
        var spawnPos = new Vector3(tracks[randomTrackID].transform.position.x, tracks[randomTrackID].transform.position.y, -40);

        var newObject = Instantiate(randomObstacle.prefab, spawnPos, Quaternion.identity);
        newObject.transform.parent = transform;
    }

   
}
