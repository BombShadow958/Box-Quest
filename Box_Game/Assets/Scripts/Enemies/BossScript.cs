using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float m_speed;
    public float m_AggroRange;
    public float m_ShootingRange;
    public float m_AwakeRange;
    public float m_FireRate = 1.0f;
    private float m_nextFireTime;
    public int BossHP = 3;
    public bool m_IsInvincible = false;
    public float m_IFrames;
    public GameObject m_bullet;
    public GameObject m_boxBullet;
    public GameObject m_bulletParent;

    [SerializeField] private BossHitCheck topCheck;
    [SerializeField] private int m_ShotType;
    [SerializeField] private bool m_Attacking;


    public Transform player;

    public PlayerControls playerX;

    public GameObject Magic; // Assign this in the Inspector
    private MagicCode magicCode; // Reference to the DoorCode script

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    private Rigidbody2D prb;

    [HideInInspector] Animator m_Anim;

    bool hasSpawned;

    public bool finalBoss;

    public void Spawned()
    {
        hasSpawned = true;
        
    }
    public AudioSource sfxSource;
    public AudioClip LaughSFX;

    public float timer;
    public float timeBetweenLaughs;


    public GameObject Door; // Assign this in the Inspector
    private DoorCode doorCode; // Reference to the DoorCode script

    void Awake()
    {
        playerX = FindObjectOfType<PlayerControls>();
        if (Magic != null)
        {
            // Get the DoorCode component from the Door GameObject
            magicCode = Magic.GetComponent<MagicCode>();
        }
        doorCode = Door.GetComponent<DoorCode>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prb = player.gameObject.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Anim = GetComponent<Animator>();
        hasSpawned = false;
        m_Attacking = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenLaughs && finalBoss == true && hasSpawned == true)
        {
            timer = 0;
            int randomNum = Random.Range(0, 2);
            sfxSource.clip = LaughSFX;
            sfxSource.Play();
        }
        if (playerX.x > 207.5f && finalBoss == true && hasSpawned == false)
        {
            magicCode.fly = true;
            while (transform.position.y > -3.19)
            {
                transform.Translate(Vector2.down * 5 * Time.deltaTime);
                break;
            }
        }

        ResetAnimDirection();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (BossHP <= 0 && finalBoss == false)
        {
            Destroy(this);
            transform.position = new Vector2(-100, -100);
        }
        else if (BossHP <= 0 && finalBoss == true) {
           
            m_Anim.SetBool("Death", true);
            m_Attacking = false;
            doorCode.reallyOpen = true;
        }

        if (distanceFromPlayer > m_AwakeRange && hasSpawned != true)
        {
            m_Anim.SetBool("Sleep", true);
        }

        if (distanceFromPlayer < m_AwakeRange && hasSpawned != true)
        {
            if (transform.position.y >= 0)
            {
                m_Anim.SetBool("Spawned", true);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            }
        }

        if (hasSpawned == true)
        {
            if (distanceFromPlayer < m_AggroRange && distanceFromPlayer > m_ShootingRange )
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, m_speed * Time.deltaTime);
                m_Anim.SetBool("Idle", true);
            }
            else if (distanceFromPlayer <= m_ShootingRange && m_nextFireTime < Time.time && !m_Anim.GetBool("Death"))
            {
                m_Anim.SetBool("Attack", true);
                m_ShotType = Random.Range(0, 2);
                if (m_Attacking == true) {
                    if (m_ShotType == 0) {
                        Instantiate(m_bullet, m_bulletParent.transform.position, Quaternion.identity);
                    }
                    else if (m_ShotType == 1) {
                        Instantiate(m_boxBullet, m_bulletParent.transform.position, Quaternion.identity);
                    }
                    m_nextFireTime = Time.time + m_FireRate;
                }
            }
        }
        if (topCheck.isColliding && m_IsInvincible == false)  {
            m_IFrames = 0.5f;
            BossHP--;
        }
        if (m_IFrames > 0) {
            m_IsInvincible = true;
            m_IFrames -= Time.deltaTime;
            rb.freezeRotation = false;
            if (player.position.x > transform.position.x) {
                prb.AddForce(Vector2.right * 1000);
                Debug.Log("right");
            }
            else if (player.position.x < transform.position.x)  {
                prb.AddForce(Vector2.left * 1000);
                Debug.Log("left");
            }
            transform.Rotate(0, 0, 15);
            sr.color = Color.red;

        }
        else  {
            transform.eulerAngles = Vector3.zero;
            rb.freezeRotation = true;
            m_IsInvincible = false;
            sr.color = Color.white;
        }
        
    }

    void ResetAnimDirection()
    {
        m_Anim.SetBool("Idle", false);
        m_Anim.SetBool("Attack", false);
        m_Anim.SetBool("Spawned", false);
        m_Anim.SetBool("Sleep", false);
        if (finalBoss) {
            m_Anim.SetBool("Death", false);
        }

    }

    void BossDies() {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_AggroRange);
        Gizmos.DrawWireSphere(transform.position, m_ShootingRange);
        Gizmos.DrawWireSphere(transform.position, m_AwakeRange);
    }
}
