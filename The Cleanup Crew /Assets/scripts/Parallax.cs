using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    const float XSTART = 43.0f; 
    public float speed = -4.0f;

    void Awake()
    {

    }

    void Update()
    {
        //xPos = this.transform.position.x;
        //yPos = this.transform.position.y;

        if (this.transform.position.x <= -XSTART/2)
        {
            this.transform.Translate(new Vector3(XSTART, 0.0f, 0.0f));
        }
        this.transform.Translate(new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
       
        
    }
}
