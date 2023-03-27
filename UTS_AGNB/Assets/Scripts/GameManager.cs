using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float initial_time;
    public float time_mult;
    public Timer timerscript;

    private float current_time;

    // Start is called before the first frame update
    void Start()
    {
        current_time = initial_time;
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
