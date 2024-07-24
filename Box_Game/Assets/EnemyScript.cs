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


    // Start is called before the first frame update
    void Start()
    {
        m_HomePoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;    
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
            Patrol();
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

    private void Patrol()
    {
        if (!isPatrolling)
        {
            ReturnHome();
        } else
        {

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
            //transform.position = m_HomePoint;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,m_AggroRange);
    }
}
