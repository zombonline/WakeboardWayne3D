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
        Track chosenTrack = null;
        switch (obstacle.obstacleType)
        {
            case ObstacleType.singleLane:
                var randomTrackID = Random.Range(0, tracks.Length);
                chosenTrack = tracks[randomTrackID];
                //random track is disabled
                if (chosenTrack.spawnDisabled)
                {
                    //each track that is enabled is added to a new temp list
                    List<Track> enabledTracks = new List<Track>();
                    foreach(Track track in tracks)
                    {
                        if(!track.spawnDisabled)
                        {
                            enabledTracks.Add(track);
                        }
                    }
                    //a new randomtrackID is chosen from that list.
                    chosenTrack = tracks[Random.Range(0, enabledTracks.Count)];

                    //if no useable track is found, unable to spawn flag is raised
                    if(chosenTrack.spawnDisabled)
                    {
                        unableToSpawn= true;
                    }
                }
                    break;

            case ObstacleType.doubleLane:
                randomTrackID = Random.Range(0, tracks.Length - 1);
                chosenTrack = tracks[randomTrackID];
                var secondaryTrack = tracks[randomTrackID + 1];

                //if randomly selected tracks disabled
                if (chosenTrack.spawnDisabled || secondaryTrack.spawnDisabled)
                {

                    //check other pair option depending on what original random pair was
                    if (randomTrackID == tracks.Length - 1)
                    {
                        chosenTrack = tracks[0];
                        secondaryTrack = tracks[1];
                    }
                    else
                    {
                        chosenTrack = tracks[1];
                        secondaryTrack = tracks[2];
                    }
                    //if no useable track is found, unable to spawn flag is raised
                    if (chosenTrack.spawnDisabled || secondaryTrack.spawnDisabled)
                    {
                        unableToSpawn = true;
                    }
                }
                    break;
            case ObstacleType.tripleLane:
                foreach(var track in tracks)
                {
                    //select centre track to spawn
                    chosenTrack = tracks[1];

                    //check if any track is disabled
                    if(track.spawnDisabled)
                    {
                        unableToSpawn = true;
                    }

                }
                break;
        }
        if(unableToSpawn)
        {
            timer = timeBetweenSpawns / 5f;
            return;
        }
        timer = timeBetweenSpawns;
        var spawnPos = new Vector3(chosenTrack.transform.position.x, chosenTrack.transform.position.y, -40);

        var newObject = Instantiate(obstacle.prefab, spawnPos, Quaternion.identity);
        newObject.transform.parent = transform;

        Destroy(newObject, 60f);
    }

   
}
