using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 Checkpoint;
    public GameObject Player;
    [SerializeField] private BoundsCheck roofCheck;
    [SerializeField] private BoundsCheck frontCheck;
    [SerializeField] private BoundsCheck backCheck;
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
        if (Input.GetKey(KeyCode.W)) {
            if (isGrounded && roofCheck.isColliding || frontCheck.isColliding && backCheck.isColliding) {
                return;
            }
            else { 
                transform.localScale += new Vector3(0.0100f, 0.0100f, 0.0100f);
                rb.mass += 0.01f;
            }
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
        if (transform.localScale == new Vector3(0.1f, 0.1f, 0.1f)) {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            rb.mass += 0.01f;
        }


       

        x = transform.position.x;
        y = transform.position.y;

        //Checkpoint

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = Checkpoint;
        }

    }
    
    public void UpdateCheckpoint(Vector2 pos)
    {
        Checkpoint = pos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
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
