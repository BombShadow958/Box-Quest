using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    public float movespeed;
    private float camX;
    public PlayerControls playerX;
    public PlayerControls playerY;
    // Start is called before the first frame update
    void Awake()
    {
        camX = 0;
        playerX = FindObjectOfType<PlayerControls>();
        playerY = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerX.x);
        if (playerX.x > transform.position.x + 9)
        {
            MoveCamRight();
        }

        if (transform.position.x < camX)
        {
            transform.Translate(Vector2.right * movespeed * Time.deltaTime);
        }
    }
    void MoveCamRight()
    {
        for (int i = 0; i < 1; i++)
        {
            camX = camX + 17.9f;
        }
    }
}
