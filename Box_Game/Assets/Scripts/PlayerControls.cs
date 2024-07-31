using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip jumpSFX;
    public AudioClip growSFX;
    public AudioClip shrinkSFX;
    public AudioClip GotHitSFX;
    public Vector2 Checkpoint;
    public GameObject Player;
    [SerializeField] private BoundsCheck roofCheck;
    [SerializeField] private BoundsCheck frontCheck;
    [SerializeField] private BoundsCheck backCheck;
    public float m_Speed;

    public float jump;
    private Rigidbody2D rb;
    [HideInInspector] public Animator m_animator;
    //float m_Horizontal;
    //float m_Vertical;
    private float maxSize = 15;
    private float minSize = 0.2f;
    private float direction; 

    public float x;
    public float y;

    [SerializeField] private bool isGroundedGround;
    [SerializeField] private bool isGroundedBox;
    private bool isFloating;

    private int m_HitPoints = 3;
    private bool m_IsInvincible = false;
    private float m_IFrames;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        m_animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResetAnimDirection();
        if (Input.GetButtonDown("Jump") && (isGroundedGround || isGroundedBox))
        {
            sfxSource.clip = jumpSFX;
            sfxSource.Play();
            rb.AddForce(Vector2.up * jump);
            m_animator.SetLayerWeight(1, 1);
            m_animator.SetBool("Jump", true);
        }

        ////Movement 
        direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * m_Speed, rb.velocity.y);


        //Size Changing
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (isGroundedGround && roofCheck.isColliding || frontCheck.isColliding && backCheck.isColliding)
            {
                return;
            }
            else
            {
                sfxSource.clip = growSFX;
                sfxSource.Play();
                transform.localScale += new Vector3(0.0100f, 0.0100f, 0.0100f);
                rb.mass += 0.01f;
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            sfxSource.clip = shrinkSFX;
            sfxSource.Play();
            transform.localScale -= new Vector3(0.0100f, 0.0100f, 0.0100f);
            rb.mass -= 0.01f;
            rb.AddForce(Vector2.up * 5);

        }

        if (transform.localScale == new Vector3(4f, 4f, 4f))
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            rb.mass -= 0.01f;
        }
        if (transform.localScale == new Vector3(0.1f, 0.1f, 0.1f))
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            rb.mass += 0.01f;
            rb.AddForce(Vector2.down * 5);
        }




        x = transform.position.x;
        y = transform.position.y;

        //Checkpoint

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = Checkpoint;
        }

        //Win Screen
        /*if (x > 81)
        {
            SceneManager.LoadSceneAsync(2);
        }*/

        // invinciblilty
        if (m_IFrames > 0)
        {
            m_IsInvincible = true;
            m_IFrames -= 1 * Time.deltaTime;
        }
        else
        {
            m_IsInvincible = false;
        }
        if (m_HitPoints == 2)
        {
            m_animator.SetBool("Hurt", true);
        }
        if (m_HitPoints == 1)
        {
            m_animator.SetBool("Injured", true);
            m_animator.SetBool("Hurt", false);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {

            // Call to the animator and set the walk layer weight to 1
            m_animator.SetLayerWeight(1, 1);

            // if the input given is Left or Right
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                // Check the value of the Horizontal Movement. Negative = left, positive = right
                if (direction < 0)
                {
                    // Left movement detected, call to animator to set the left bool to true
                    m_animator.SetBool("Left", true);
                }
                else
                {
                    m_animator.SetBool("Right", true);
                }
            }
        }
        else // no movement detected
        {
            m_animator.SetLayerWeight(0, 0);
            m_animator.SetLayerWeight(1, 0);
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        Checkpoint = pos;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGroundedGround = true;
            m_animator.SetBool("Jump", false);
        }
        if (other.gameObject.CompareTag("Box"))
        {
            isGroundedBox = true;
            m_animator.SetBool("Jump", false);
        }

        //Lose Screen 
        if (other.gameObject.CompareTag("Enemy") && m_IsInvincible == false) {
            if (m_HitPoints == 1) {
                SceneManager.LoadSceneAsync(3);
            }
            else {
                sfxSource.clip = GotHitSFX;
                sfxSource.Play();
                m_IFrames = 1.5f;
                m_HitPoints--;
                transform.position = Checkpoint;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && (!frontCheck.isColliding || !backCheck.isColliding))
        {
            isGroundedGround = false;
        }
        if ( other.gameObject.CompareTag("Box") && (!frontCheck.isColliding || !backCheck.isColliding))
        {
            isGroundedBox = false;
        }
    }

    private void FixedUpdate()
    {
        //m_Horizontal = Input.GetAxis("Horizontal");

        //rb.velocity = new Vector2(m_Horizontal * m_Speed, m_Vertical * m_Speed);
    }
    void ResetAnimDirection()
    {
        m_animator.SetBool("Left", false);
        m_animator.SetBool("Right", false);
    }
}
