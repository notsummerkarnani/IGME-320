using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float elapsedTime;
    float reloadTime;
    public GameObject bulletPrefab;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Drone");
        elapsedTime = Random.Range(0f,5f);
        reloadTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vector3.Normalize(target.transform.position - gameObject.transform.position);
        //transform.forward = transform.up;

        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;               //ignore collisions
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;        //ignore collisions 


        elapsedTime += Time.deltaTime;
        if (elapsedTime >= reloadTime)
        {
            elapsedTime = 0;
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
            bullet.transform.LookAt(target.transform);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("Trigger: " + collider.gameObject.name);
        if(collider.gameObject.name == "Sphere(Clone)")
        {
            this.gameObject.SetActive(false);
        }
    }

}
