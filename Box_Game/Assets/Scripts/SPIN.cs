using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPIN : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool goLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -7) {
            goLeft = false; 
        }
        if (transform.position.x >= 7)
        {
            goLeft = true;
        }

        if (goLeft == true) {
            rb.AddForce(Vector2.left * 10);
        }
        else {
            rb.AddForce(Vector2.right * 10);
        }
        transform.Rotate(0, 0, 10);
    }
}
