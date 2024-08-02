using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCode : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip openSFX;
    public AudioClip closeSFX;
    public bool open;
    public bool holdOpen;
    public bool timerOpen;
    public bool reallyOpen;
    public float timer;
    public float doorTimer;
    private float maxHeight;
    // Start is called before the first frame update
    void Awake()
    {
        maxHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Open
        if (open == true && transform.position.y < maxHeight + 2.0f)
        {
            sfxSource.clip = openSFX;
            sfxSource.Play();
            transform.Translate(Vector2.up * 5 * Time.deltaTime);
        }
        //Hold Open
        if (holdOpen == true && open == false && transform.position.y > maxHeight)
        {
            sfxSource.clip = closeSFX;
            sfxSource.Play();
            transform.Translate(Vector2.down * 5 * Time.deltaTime);
        }
        //Timer Open
        if (timerOpen == true && open == true) 
        {
            
            while (transform.position.y < maxHeight + 2.0f)
            {
                sfxSource.clip = openSFX;
                sfxSource.Play();
                transform.Translate(Vector2.up * 5 * Time.deltaTime);
                if (transform.position.y > maxHeight + 2.0f) {
                    break; 
                }
            }
            timer = 0;
        }
        if (timerOpen == true && timer > doorTimer)
        {
            if (open == false && transform.position.y > maxHeight)
            {
                sfxSource.clip = closeSFX;
                sfxSource.Play();
                transform.Translate(Vector2.down * 5 * Time.deltaTime);
            }
        }
        //Really Open
        if (reallyOpen == true)
        {
            sfxSource.clip = openSFX;
            sfxSource.Play();
            transform.Translate(Vector2.up * 5 * Time.deltaTime);
        }
    }
}
