using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerControls playerControls;

    [HideInInspector] Animator m_Anim;

    public AudioSource sfxSource;
    public AudioClip touchSFX;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        m_Anim = GetComponent<Animator>();

        m_Anim.SetBool("Activated", false);
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sfxSource.clip = touchSFX;
            sfxSource.Play();
            playerControls.UpdateCheckpoint(transform.position);
            m_Anim.SetBool("Activated", true);
        }
    }
}
