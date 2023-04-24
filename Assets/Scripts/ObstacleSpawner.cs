using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Obstacle[] obstacle, ramp, railing, misc;
    [SerializeField] Track[] tracks;
    [SerializeField] float timeBetweenSpawns;

    [SerializeField] float obstacleChance, rampChance, railingChance, miscChance;
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
            if (randomNumber < obstacleChance)
            {
                SpawnObstacle(obstacle);
            }
            else if(randomNumber < obstacleChance + rampChance)
            {
                SpawnObstacle(ramp);
            }
            else if(randomNumber < obstacleChance + rampChance + railingChance)
            {
                SpawnObstacle(railing);
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
                if (tracks[randomTrackID].trackOccupied)
                {
                    unableToSpawn = true;
                }
                else
                {
                    tracks[randomTrackID].DelayTrackSpawning(randomObstacle.laneSpawnDelay);
                }
                    break;

            case ObstacleType.doubleLane:
                randomTrackID = Random.Range(0, tracks.Length-1);
                if (tracks[randomTrackID].trackOccupied || tracks[randomTrackID + 1].trackOccupied)
                {
                    unableToSpawn = true;
                }
                else
                {
                    tracks[randomTrackID].DelayTrackSpawning(randomObstacle.laneSpawnDelay);
                    tracks[randomTrackID + 1].DelayTrackSpawning(randomObstacle.laneSpawnDelay);
                }
                    break;
            case ObstacleType.tripleLane:
                randomTrackID = 0;
                if (tracks[randomTrackID].trackOccupied || tracks[randomTrackID + 1].trackOccupied || tracks[randomTrackID+2].trackOccupied)
                {
                    unableToSpawn = true;
                }
                else
                {
                    tracks[randomTrackID].DelayTrackSpawning(randomObstacle.laneSpawnDelay);
                    tracks[randomTrackID + 1].DelayTrackSpawning(randomObstacle.laneSpawnDelay);
                    tracks[randomTrackID + 2].DelayTrackSpawning(randomObstacle.laneSpawnDelay);
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
