using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerControls playerControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerControls.UpdateCheckpoint(transform.position);
            
        }
    }
}
