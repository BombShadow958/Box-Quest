using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitCheck : MonoBehaviour
{

    public bool isColliding { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Box") && !collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Untagged"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}
