using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    Vector3 position;
    Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5)
            position = new Vector3(20.0f, 4.5f, -1.5f);
        else
            position = new Vector3(20.0f, -4.5f, -1.5f);

        speed = new Vector3(-4.0f, 0f, 0f);

        transform.position = Vector3.Lerp(transform.position, position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, position, 1);

        if (transform.position.x < -20.0f)
            Destroy(this.gameObject);
    }
}
