using GLTF.Schema;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Timer timerscript;

    public float initial_time;
    public float time_mult;
    public GameObject player;
    public GameObject flowers;

    public GameObject[] spawns;

    private float current_time;

    private const int COINS_AMOUNT = 8;
    private const float MAX_SPAWNRANGE = 300f;
    private const float DECAY_SEED = 0.2f;
    private const float EULER = 2.71828f;

    int flowers_left = 0;

    class spawnpoints
    {
        Transform spawn;
        float distance;

        public spawnpoints(Transform a, float b)
        {
            spawn = a;
            distance = b;
        }
        public float getDist()
        {
            return distance;
        }

        public Transform getSpawn()
        {
            return spawn;
        }
    }

    private float getRandFloat(float min, float max)
    {
        float r = (float) UnityEngine.Random.Range(min, max+1)/max+1;
        return r;
    }

    public void decrementFlowers()
    {
        flowers_left--;
    }

    public int getFlowersLeft()
    {
        return flowers_left;
    }

    //generates N objects that act as collectible coins in a certain distance from the player
    public void generateCoins(int amount)
    {
        flowers_left = amount;

        //create a generic float list to store distance between player and spawns
        List<spawnpoints> distanceArray = new List<spawnpoints>();
        spawnpoints temp;

        //for all spawn points inside array
        for (int i = 0; i < spawns.Length; i++)
        {
            //adds the magnitude of vector3 that is the vector of distance between a player and all spawn points
            temp = new spawnpoints(
                spawns[i].transform,
                (player.transform.position - spawns[i].transform.position).magnitude
            );
            distanceArray.Add(temp);
        }

        //sort the distance array descending
        distanceArray.Sort((a, b) => a.getDist().CompareTo(b.getDist()));

        //create a list to store probabilities of each spawnpoints
        List<float> probabilities = new List<float>();
        float totalprobability = 0f;
        float probability;

        //the probabilities increase as distance decrease
        foreach (spawnpoints points in distanceArray)
        {
            probability = 1f / (points.getDist() + 1f);
            probabilities.Add(probability);
            //get sum of all probabilities
            totalprobability += probability;
        }

        for (int i = 0; i < COINS_AMOUNT; i++)
        {
            totalprobability = 0;
            for (int k = 0; k < probabilities.Count; k++)
            {
                totalprobability += probabilities[k];
            }
            //normalize probabilities to equal 1
            for (int k = 0; k < probabilities.Count; k++)
            {
                probabilities[k] /= totalprobability;
            }

            //create random double
            float sum = 0f;
            //for every spawnpoints
            for (int j = 0; j < probabilities.Count; j++)
            {
                //sum of all spawns that have been checked
                sum += probabilities[j];
                float r = (getRandFloat(0, 10000) / 10001);
                if (r <= sum)
                {
                    Instantiate(flowers, distanceArray[j].getSpawn().position, distanceArray[j].getSpawn().rotation);
                    distanceArray.RemoveAt(j);
                    break;
                }
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        current_time = initial_time;
        generateCoins(COINS_AMOUNT);
    }

    // Update is called once per frame
    void Update()
    {
        current_time -= Time.deltaTime;
        timerscript.updateTimer(current_time, initial_time);

        if (current_time <= 0)
        {
            //game over
        }
    }


}
