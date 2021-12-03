using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bullet : MonoBehaviour
{
    private Vector3 position;
    public  Vector3 velocity;
    float time;
    const float MAXTIME = 4.0f;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position + transform.forward * 2; 
        velocity = gameObject.transform.forward * 25;
        canvas = GameObject.Find("Canvas");
        transform.position = Vector3.Lerp(transform.position, position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= MAXTIME)
        {
            Destroy(gameObject);
        }
        position = transform.position;
        position += velocity * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, position, 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("col: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        if (collision.gameObject.tag == "Player")
        {
            canvas.GetComponent<score>().AddPenalty();
        }
    }
}
