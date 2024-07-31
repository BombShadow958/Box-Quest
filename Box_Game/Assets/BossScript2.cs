using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BossScript2 : MonoBehaviour
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

    private Transform player;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    [HideInInspector] Animator m_Anim;

    bool hasSpawned;

    public bool finalBoss;

    public void Spawned()
    {
        hasSpawned = true;
        finalBoss = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Anim = GetComponent<Animator>();
        hasSpawned = false;

    }

    // Update is called once per frame
    void Update()
    {

        ResetAnimDirection();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (BossHP <= 0 && finalBoss == false)
        {
            Destroy(this);
            transform.position = new Vector2(-100, -100);
        }
        else if (BossHP <= 0 && finalBoss == true) {
            //Destroy(this);
            m_Anim.SetBool("Death", true);
        }

        if (distanceFromPlayer > m_AwakeRange && hasSpawned != true)
        {
            m_Anim.SetBool("Sleep", true);
        }

        if (distanceFromPlayer < m_AwakeRange && hasSpawned != true)
        {
            m_Anim.SetBool("Spawned", true);

            if (transform.position.y >= 0)
            {

            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            }
        }

        if (hasSpawned == true)
        {
            if (distanceFromPlayer < m_AggroRange && distanceFromPlayer > m_ShootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, m_speed * Time.deltaTime);
                m_Anim.SetBool("Idle", true);
            }
            else if (distanceFromPlayer <= m_ShootingRange && m_nextFireTime < Time.time)
            {
                m_Anim.SetBool("Attack", true);
                m_ShotType = Random.Range(0, 2);
                if (m_ShotType == 0)
                {
                    Instantiate(m_bullet, m_bulletParent.transform.position, Quaternion.identity);
                }
                else if (m_ShotType == 1) {
                    Instantiate(m_boxBullet, m_bulletParent.transform.position, Quaternion.identity);
                }
                m_nextFireTime = Time.time + m_FireRate;
            }
        }
        if (topCheck.isColliding && m_IsInvincible == false)  {
            m_IFrames = 2.5f;
            if (BossHP == 0) {
               // Destroy(this.gameObject);
            }
            BossHP--;
        }
        if (m_IFrames > 0) {
            m_IsInvincible = true;
            m_IFrames -= Time.deltaTime;
            rb.freezeRotation = false;
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
        m_Anim.SetBool("Death", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_AggroRange);
        Gizmos.DrawWireSphere(transform.position, m_ShootingRange);
        Gizmos.DrawWireSphere(transform.position, m_AwakeRange);
    }
}
