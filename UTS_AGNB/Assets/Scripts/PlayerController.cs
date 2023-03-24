using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Global variables
    public CharacterController controls;

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
            movement_speed = running_speed;
        }
        //use crouching speed
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            movement_speed = crouching_speed;
        }
        
        //use walking speed
        else
        {
            movement_speed = walking_speed;
        }
        Vector3 direction = transform.right * horizontalInput + transform.forward * verticalInput;

        controls.Move(direction * movement_speed * Time.deltaTime);
    }
}
