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

    // Start is called before the first frame update
    void Start()
    {
        m_HomePoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
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
            transform.position = Vector2.MoveTowards(this.transform.position, m_HomePoint, m_speed * Time.deltaTime);
        }
    }

    private void Patrolling()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint = pointB.transform)
        {
            rb.velocity = new Vector2(m_speed, 0);
        } else
        {
            rb.velocity = new Vector2(-m_speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,m_AggroRange);

        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    }
}
