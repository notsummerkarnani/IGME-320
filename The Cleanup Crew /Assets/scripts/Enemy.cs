using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float LOWESTSPEED = 8.0f;
    const float HIGHESTSPEED = 14.0f;

    Vector3 position;
    public float speed;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        speed = Random.Range(LOWESTSPEED, HIGHESTSPEED);

        if (Random.value > 0.5f)        
        {
            position = new Vector3(-25.0f, GameManager.lanes[(int)((Random.value * 10) % GameManager.lanes.Length/2)], 0f);
        }
        else
        {
            speed = -speed;
            position = new Vector3(25.0f, GameManager.lanes[(int)((Random.value * 10) % GameManager.lanes.Length / 2) + GameManager.lanes.Length / 2], 0f);
        }

        this.transform.position = Vector3.Lerp(this.transform.position, position, 1);

    }

    // Update is called once per frame
    void Update()
    {
        position = this.transform.position;
        position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
        this.transform.position = Vector3.Lerp(this.transform.position, position, 1);

        if((this.transform.position.x > 21.5 && speed>0) || (this.transform.position.x < -21.5 && speed < 0) || !isAlive)
        {
            Destroy(gameObject);
        }
    }

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(speed>0 && collision.collider.transform.position.x < collision.otherCollider.transform.position.x)
            {
                speed += collision.collider.gameObject.GetComponent<Enemy>().speed - collision.otherCollider.gameObject.GetComponent<Enemy>().speed +1 ;
            }
            else if (speed < 0 && collision.collider.transform.position.x < collision.otherCollider.transform.position.x)
            {
                collision.collider.gameObject.GetComponent<Enemy>().speed += (collision.otherCollider.gameObject.GetComponent<Enemy>().speed - collision.collider.gameObject.GetComponent<Enemy>().speed - 1);
            }
        }
        else if(collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }
    }
}
