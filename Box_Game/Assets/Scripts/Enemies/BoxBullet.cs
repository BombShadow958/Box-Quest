using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBullet : MonoBehaviour
{

    GameObject target;
    [SerializeField] private GameObject HitBox;
    public float m_speed;
    Rigidbody2D bulletRB;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * m_speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    private void OnCollisionEnter2D(Collision2D other)  {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))  {
            this.tag = "Ground";
            bulletRB.gravityScale = 1;
            bulletRB.mass = 10;
        }
    }
}
