using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector3 pos;
    Vector3 speed;
    public float dodgeSpeed;
    int laneIndex;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        pos = Vector3.up;
        speed = Vector3.zero;
        dodgeSpeed = 8f;
        laneIndex = 2;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        pos = this.transform.position;
        //cam.transform.LookAt(this.gameObject.transform, Vector3.up);
        if (!Input.anyKey)
        {
            speed = Vector3.zero;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SwitchLane(KeyCode.W);
                //cam.transform.LookAt(this.gameObject.transform,Vector3.up);
                return;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SwitchLane(KeyCode.S);
               // cam.transform.LookAt(this.gameObject.transform, Vector3.up);
                return;
            }
            if (Input.GetKey(KeyCode.A))
            {
                speed = new Vector3(-dodgeSpeed, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                speed = new Vector3(dodgeSpeed, 0f, 0f);
            }
        }

        pos += speed * Time.deltaTime;
        if (pos.x > 8.5)
            pos.x = 8.5f;
        if (pos.x < -8.5)
            pos.x = -8.5f;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, 1);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"Player collider:: {collider.tag}");

        if (collider.gameObject.tag == "Item")
        {
            score.PickUp();
            Destroy(collider.gameObject);
            dodgeSpeed -= 0.05f;
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            GameManager.endScene = true;
        }
        else if (collider.gameObject.tag == "Bin")
        {
            score.Deposit();
            dodgeSpeed = 8.0f;
            collider.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }

    }

    void SwitchLane(KeyCode key)
    {
        pos = transform.position;
        
        if (key == KeyCode.W && laneIndex < GameManager.lanes.Length-1)
        {
            laneIndex++;
        }
        else if(key == KeyCode.S && laneIndex > 0)
        {
            laneIndex--;
        }
        pos.y = GameManager.lanes[laneIndex];
        this.transform.position = Vector3.Lerp(this.transform.position, pos, 1);
    }
}
