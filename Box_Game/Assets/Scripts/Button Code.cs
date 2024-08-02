using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip pressSFX;
    public GameObject Door; // Assign this in the Inspector
    private DoorCode doorCode; // Reference to the DoorCode script
    [HideInInspector] public Animator m_animator;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        if (Door != null)
        {
            // Get the DoorCode component from the Door GameObject
            doorCode = Door.GetComponent<DoorCode>();
        }
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            sfxSource.clip = pressSFX;
            sfxSource.Play();
            m_animator.SetBool("Pressed", true);
            if (doorCode != null)
            {
                doorCode.open = true; // Change the boolean value in DoorCode
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            m_animator.SetBool("Pressed", false);
            if (doorCode != null)
            {
                doorCode.open = false; // Optionally reset the boolean when the player or box exits
            }
        }
    }
}