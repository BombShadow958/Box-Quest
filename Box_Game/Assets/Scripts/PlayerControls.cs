using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float m_Speed;

    public float jump;
    private Rigidbody2D rb;
    float m_Horizontal;
    float m_Vertical;
    private float maxSize = 15;
    private float minSize = 0.2f;

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
        if (Input.GetKey(KeyCode.W)) {
            transform.localScale += new Vector3(0.0100f, 0.0100f, 0.0100f);
            rb.mass += 0.01f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.localScale -= new Vector3(0.0100f, 0.0100f, 0.0100f);
            rb.mass -= 0.01f;
        }

        if (transform.localScale == new Vector3(4f, 4f, 4f)) {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            rb.mass -= 0.01f;
        }
        if (transform.localScale == new Vector3(0.5f, 0.5f, 0.5f)) {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            rb.mass += 0.01f;
        }

        x = transform.position.x;
        y = transform.position.y;
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        m_Horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(m_Horizontal * m_Speed, m_Vertical * m_Speed);
    }
}
