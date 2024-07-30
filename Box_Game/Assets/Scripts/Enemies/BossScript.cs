using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float m_speed;
    public float m_AggroRange;
    public float m_ShootingRange;
    public float m_AwakeRange;
    public float m_FireRate = 1.0f;
    private float m_nextFireTime;
    public GameObject m_bullet;
    public GameObject m_bulletParent;

    private Transform player;

    private Rigidbody2D rb;

    [HideInInspector] Animator m_Anim;

    bool hasSpawned;

    public void Spawned()
    {
        hasSpawned = true;
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

        if (distanceFromPlayer > m_AwakeRange && hasSpawned != true)
        {
            m_Anim.SetBool("Sleep", true);
        }

        if (distanceFromPlayer < m_AwakeRange && hasSpawned != true)
        {
            m_Anim.SetBool("Spawned", true);
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
                Instantiate(m_bullet, m_bulletParent.transform.position, Quaternion.identity);
                m_nextFireTime = Time.time + m_FireRate;
            }
        }
    }

    void ResetAnimDirection()
    {
        m_Anim.SetBool("Idle", false);
        m_Anim.SetBool("Attack", false);
        m_Anim.SetBool("Spawned", false);
        m_Anim.SetBool("Sleep", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_AggroRange);
        Gizmos.DrawWireSphere(transform.position, m_ShootingRange);
        Gizmos.DrawWireSphere(transform.position, m_AwakeRange);
    }
}
