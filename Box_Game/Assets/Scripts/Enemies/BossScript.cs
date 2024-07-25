using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float m_speed;
    public float m_AggroRange;
    public float m_ShootingRange;
    public float m_FireRate = 1.0f;
    private float m_nextFireTime;
    public GameObject m_bullet;
    public GameObject m_bulletParent;

    private Transform player;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < m_AggroRange && distanceFromPlayer > m_ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, m_speed * Time.deltaTime);

        }
        else if (distanceFromPlayer <= m_ShootingRange && m_nextFireTime < Time.time)
        {
            Instantiate(m_bullet,m_bulletParent.transform.position, Quaternion.identity);
            m_nextFireTime = Time.time + m_FireRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_AggroRange);
        Gizmos.DrawWireSphere(transform.position, m_ShootingRange);
    }
}
