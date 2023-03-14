using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Global variables
    public CharacterController controls;

    public float movement_speed = 15f;

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

        Vector3 direction = transform.right * horizontalInput + transform.forward * verticalInput;

        controls.Move(direction * movement_speed * Time.deltaTime);
    }
}
