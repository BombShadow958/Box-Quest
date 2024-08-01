using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCode : MonoBehaviour
{
    public bool fly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fly == true)
        {
            transform.Translate(Vector2.down * 5 * Time.deltaTime);
        }
    }
}
