using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAction : MonoBehaviour
{
    public SFXManager sfxmanager;
    public TextOverlayManager textoverlaymanager;
    public GameManager gamemanager;

    private void OnTriggerEnter(Collider other)
    {
        //when collides with player
        if (other.name == "Player")
        {
            Debug.Log("Collides with " + other.name);

            sfxmanager.playSFX(SFXManager.clips.DING);
            textoverlaymanager.startTextOverlay(gamemanager.getFlowersLeft());
            gamemanager.decrementFlowers();

            //destroy self
            Destroy(gameObject);
        }
    }
}
