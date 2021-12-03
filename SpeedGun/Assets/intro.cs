using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    GameObject player;
    Text text;
    GameObject turret;
    GameObject ring;


    void Start()
    {
        player = GameObject.Find("Drone");
        text = GetComponentInChildren<Text>();
        GameObject.Find("Arrow").SetActive(false);
        turret = GameObject.Find("Turret");
        ring = GameObject.Find("Ring");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 100)
        {
            text.text = "Rings turn green when they're activated.";

            if (!turret.activeInHierarchy)
            {
                ring.GetComponent<MeshRenderer>().material.color = Color.green;

                text.text = "Pass through the ring to go to the course.";
                if (ring.GetComponent<MeshCollider>().bounds.Contains(player.transform.position))
                {
                    SceneManager.LoadScene(1);
                }
            }
            return;
        }
        if (player.transform.position.z > 80)
        {
            text.text = "Shoot the turret to activate the ring.";
            return;
        }
        if (player.transform.position.z > 60)
        {
            text.text = "Make sure not to get hit or you take a 2 second penalty!";
            return;
        }
        if (player.transform.position.z > 20)
        {
            text.text = "Hold shift for speed.\nLeft click the mouse to shoot.";
            return;
        }
        if (player.transform.position.z >=0)
        {
            text.text = "Use WASD to move.\nMove the mouse to rotate.\nQ and E to ascend/descend.";
        }
    }
}
