using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobControls : MonoBehaviour
{
    public bool _enabled = true;

    public float amplitude = 0.015f;
    public float frequency = 10f;
    public float togglespeed = 3f;

    public Transform playercamera;
    public Transform cameraholder;
    public CharacterController player;

    private Vector3 startposition;

    private void PlayMotion(Vector3 motion)
    {
        playercamera.localPosition += motion;
    }
    private Vector3 footstep()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private void checkMotion()
    {
        float speed = new Vector3(player.velocity.x, 0, player.velocity.z).magnitude;

        if (speed < togglespeed) return;

        PlayMotion(footstep());
    }

    private void resetPosition()
    {
        if (playercamera.localPosition == startposition) return;
        playercamera.localPosition = Vector3.Lerp(playercamera.localPosition, startposition, 1 * Time.deltaTime);
    }

    private Vector3 focusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraholder.localPosition.y, transform.position.z);
        pos += cameraholder.forward * 15f;
        return pos;
    }    


    // Start is called before the first frame update
    void Start()
    {
        startposition = playercamera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enabled) return;

        checkMotion();
        resetPosition();
        playercamera.LookAt(focusTarget());
    }
}
