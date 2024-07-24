using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALLYCONTROLLER : MonoBehaviour
{
    public GameObject Checkpoint = null;
    public GameObject Player;
    public float m_Speed;

    public float jump;
    private Rigidbody2D rb;
    //float m_Horizontal;
    //float m_Vertical;
    private float maxSize = 15;
    private float minSize = 0.2f;
    private float direction;

    public float x;
    public float y;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jump);
        }

        ////Movement 
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * m_Speed, rb.velocity.y);

        //Size Changing

        x = transform.position.x;
        y = transform.position.y;

        //Checkpoint

        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("teleported");
            Player.transform.position = Checkpoint.transform.position;
            y = -3.5f;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            Checkpoint = GameObject.Find("Checkpoint");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Box"))
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        //m_Horizontal = Input.GetAxis("Horizontal");

        //rb.velocity = new Vector2(m_Horizontal * m_Speed, m_Vertical * m_Speed);
    }
}


