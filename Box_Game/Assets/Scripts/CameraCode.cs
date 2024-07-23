using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    public float movespeed;
    private float camX;
    public PlayerControls playerX;
    public PlayerControls playerY;
    bool moveForward;
    // Start is called before the first frame update
    void Awake() {
        camX = 0;
        playerX = FindObjectOfType<PlayerControls>();
        playerY = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(playerX.x);
        if (playerX.x > camX + 8) {
            moveForward = true;
            MoveCamRight();
        }
        if (playerX.x < camX - 8) {
            moveForward = false;
            MoveCamLeft();
        }

        if (transform.position.x < camX && moveForward == true) {
            transform.Translate(Vector2.right * movespeed * Time.deltaTime);
        }
        if (transform.position.x > camX && moveForward == false)
        {
            transform.Translate(Vector2.left * movespeed * Time.deltaTime);
        }
    }
    void MoveCamRight() {
            camX += 18.0f;
    }
    void MoveCamLeft() {
        camX -= 18.0f;
    }
}
