using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float m_speed;
    public float m_AggroRange;

    private Transform player;

    [SerializeField] Transform m_CastPoint;

    bool isPatrolling;
    bool isHome;

    Vector2 m_HomePoint;

    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    private Rigidbody2D rb;

    private float m_ChangeDirectionTimer = 3.0f;
    bool m_TurnAround;
    private float m_TimeIncrease;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_HomePoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_TurnAround = false;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < m_AggroRange)
        {
            ChasePlayer();
        } else
        {
            PatrolOrHome();
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        var mCastDist = distance;

        Vector2 mEndPos = m_CastPoint.position + Vector3.right * distance;

        RaycastHit2D hit = Physics2D.Linecast(m_CastPoint.position, mEndPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                //Aggro sees enemy
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(m_CastPoint.position, mEndPos, Color.blue);
        }
        return val;
    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing");
        isPatrolling = false;
        isHome = false;
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, m_speed * Time.deltaTime);
    }

    private void PatrolOrHome()
    {
        if (!isPatrolling)
        {
            ReturnHome();
        } else
        {
            Patrolling();
        }
    }

    private void ReturnHome()
    {
        if (isHome)
        {
            isPatrolling = true;
        }
        else
        {
            Debug.Log("Going Home");
            transform.position = Vector2.MoveTowards(this.transform.position, m_HomePoint, m_speed * Time.deltaTime);
            if (transform.position.x == m_HomePoint.x && transform.position.y == m_HomePoint.y)
            {
                isHome = true;
            }
        }
    }

    private void Patrolling()
    {
        Debug.Log("Patrolling");

        m_TimeIncrease += Time.deltaTime;

        
        if (m_TurnAround && m_ChangeDirectionTimer == m_TimeIncrease)
        {
            transform.position = new Vector2(m_speed, transform.position.y);
            //rb.velocity = new Vector2(m_speed, rb.velocity.y); // Move to the right
            m_TurnAround = false;
        }
        else if (m_TurnAround && m_ChangeDirectionTimer == m_TimeIncrease)
        {
            transform.position = new Vector2(-m_speed, transform.position.y);
            //rb.velocity = new Vector2(-m_speed, rb.velocity.y); // Move to the left
            m_TurnAround = true;
        }

        /*m_timer += Time.deltaTime;

        if (m_timer >= 180 && m_TurnAround == false)
        {
            m_timer = 0;
            transform.position = new Vector2(m_speed, 0);
            m_TurnAround = true;
        }
        else if (m_timer >= 180 && m_TurnAround == true)
        {
            m_timer = 0;
            transform.position = new Vector2(-m_speed, 0);
            m_TurnAround = false;
        }*/

        /*Vector2 point = currentPoint.position - transform.position;
        if (currentPoint = pointB.transform)
        {
            transform.position = new Vector2(m_speed, 0);
        } else
        {
            transform.position = new Vector2(-m_speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,m_AggroRange);

        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    }
}
