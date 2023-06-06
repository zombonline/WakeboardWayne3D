using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Obstacle[] obstacles;
    [SerializeField] Track[] tracks;
    [SerializeField] float timeBetweenSpawns;

    float maxChanceValue;
    float timer;
    public float timerEffector;

    private void Awake()
    {
        timer = timeBetweenSpawns;

        foreach(var obstacle in obstacles)
        {
            maxChanceValue += obstacle.chanceToSpawn;
        }
    }

    public void ResetObstacles()
    {
        timer = 5f;
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach(Track track in FindObjectsOfType<Track>())
        {
            track.spawnDisabled= false;
            track.GetComponentInChildren<RailCheck>().railingActive= false;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime * timerEffector;
        if(timer <= 0)
        {
            var randomNumber = Random.Range(0,maxChanceValue);
            var number = 0f;
            foreach (var obstacle in obstacles)
            {
                number += obstacle.chanceToSpawn;
                if(number >= randomNumber)
                {
                    SpawnObstacle(obstacle);
                    break;
                }
            }
        }
    }
    private void SpawnObstacle(Obstacle obstacle)
    {
        bool unableToSpawn = false;
        var randomTrackID = 0;
        switch (obstacle.obstacleType)
        {
            case ObstacleType.singleLane:
                randomTrackID = Random.Range(0, tracks.Length);
                if (tracks[randomTrackID].spawnDisabled)
                {
                    unableToSpawn = true;
                }
                    break;

            case ObstacleType.doubleLane:
                randomTrackID = Random.Range(0, tracks.Length-1);
                if (tracks[randomTrackID].spawnDisabled || tracks[randomTrackID + 1].spawnDisabled)
                {
                    unableToSpawn = true;
                }
                    break;
            case ObstacleType.tripleLane:
                randomTrackID = 1;
                if (tracks[randomTrackID].spawnDisabled || tracks[randomTrackID + 1].spawnDisabled || tracks[randomTrackID-1].spawnDisabled)
                {
                    unableToSpawn = true;
                }
                break;

        }
        if(unableToSpawn)
        {
            timer = timeBetweenSpawns / 2f;
            return;
        }
        timer = timeBetweenSpawns;
        var spawnPos = new Vector3(tracks[randomTrackID].transform.position.x, tracks[randomTrackID].transform.position.y, -40);

        var newObject = Instantiate(obstacle.prefab, spawnPos, Quaternion.identity);
        newObject.transform.parent = transform;

        Destroy(newObject, 60f);
    }

   
}
