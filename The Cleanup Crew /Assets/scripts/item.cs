using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    float speed = -4.0f;
    Vector3 position;
    Vector3 velocity;
    float screenWidth = 22;
    public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(12 + (Random.value * screenWidth), GameManager.lanes[(int)((Random.value*20)%4)], 0);
        velocity = new Vector3(speed, 0f, 0f);
        isAlive = true;
        
        transform.position = Vector3.Lerp(transform.position, position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position += velocity * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, position, 1);

        if(position.x < -screenWidth || !isAlive)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }
    }
}
