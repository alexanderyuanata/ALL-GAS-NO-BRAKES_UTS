using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    // Global variables
    public CharacterController controls;
    public HeadBobControls headBobControls;
    public GameObject stopwatch;
    public GameObject held;
    public GameObject stored;

    public AudioManager audioManager;

    float movement_speed;
    public float walking_speed;
    public float running_speed;
    public float crouching_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gets movement inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //use running speeed
        if(Input.GetKey(KeyCode.LeftShift))
        {
            headBobControls.frequency = 12;
            movement_speed = running_speed;
        }
        //use crouching speed
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            headBobControls.frequency = 5;
            movement_speed = crouching_speed;
        }
        //use walking speed
        else
        {
            headBobControls.frequency = 8;
            movement_speed = walking_speed;
        }
        Vector3 direction = transform.right * horizontalInput + transform.forward * verticalInput;


        // if right click is held
        if (Input.GetKey(KeyCode.Mouse1))
        {
            stopwatch.transform.position = held.transform.position;
            audioManager.changeVolume(100);
        }
        else
        {
            audioManager.changeVolume(0);
            stopwatch.transform.position = stored.transform.position;
        }

        controls.Move(direction * movement_speed * Time.deltaTime);
    }
}
