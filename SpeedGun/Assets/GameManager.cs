using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float length;
    float width;
    float maxDeviation;
    float angle;
    float lapTime;
    float lastLapTime;

    int ringCount;
    int targetIndex;

    public GameObject ring;
    public GameObject turret;
    public GameObject player;
    public GameObject canvas;
    GameObject arrow;

    List<GameObject> rings;
    List<GameObject> turrets;
    


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWaypoint();
        arrow.transform.LookAt(rings[targetIndex].transform, rings[targetIndex].transform.forward);
        arrow.GetComponent<MeshRenderer>().material.color = rings[targetIndex].GetComponent<MeshRenderer>().material.color;
        //Debug.Log(lapTime);
    }

    void GenerateTrack()
    {
        for (int i = 0; i < ringCount; i++)
        {
            rings.Add(Instantiate(ring));
            
            float newAngle = angle * i * Mathf.Deg2Rad;
            rings[i].transform.position = new Vector3(Mathf.Sin(newAngle) * width, 0, Mathf.Cos(newAngle) * length);
            rings[i].transform.Rotate(0, newAngle * Mathf.Rad2Deg + 90.0f, 0);
            rings[i].transform.localScale = new Vector3(0.3f, 0.3f, 2.0f);

            Vector3 deviation = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * maxDeviation;
            rings[i].transform.position += deviation;

            turrets.Add(Instantiate(turret));
            turrets[i].transform.position = rings[i].transform.position + Vector3.up*5;

            rings[i].SetActive(true);
            turrets[i].SetActive(true);


            rings[i].GetComponent<MeshRenderer>().material.color = Color.red;
            turrets[i].GetComponent<MeshRenderer>().material.color = Color.red;

        }
    }

    void CheckWaypoint()
    {
        rings[targetIndex].GetComponent<MeshRenderer>().material.color = Color.yellow;
        if (turrets[targetIndex].activeInHierarchy)
        {
            return;
        }
        rings[targetIndex].GetComponent<MeshRenderer>().material.color = Color.green;
        //turrets[targetIndex].GetComponent<MeshRenderer>().material.color = Color.yellow;

        if (rings[targetIndex].GetComponent<MeshCollider>().bounds.Contains(player.transform.position))
        {
            if(targetIndex == 0)
            {
                canvas.GetComponent<score>().StartClock();
            }

            rings[targetIndex].GetComponent<MeshRenderer>().material.color = Color.blue;
            turrets[targetIndex].GetComponent<MeshRenderer>().material.color = Color.blue;

            if (targetIndex < ringCount - 1)
            {
                targetIndex++;
            }
            else
            {
                targetIndex = 0;

                canvas.GetComponent<score>().StopClock();
                foreach (GameObject ring in rings)
                {
                    if (ring)
                        Destroy(ring);
                }
                foreach (GameObject turret in turrets)
                {
                    if (turret)
                        Destroy(turret);
                }
                Reset();
            }
        }
    }

    void Reset()
    {
        length = 100;
        width = 30;
        ringCount = 10;
        maxDeviation = 2;

        rings = new List<GameObject>();
        turrets = new List<GameObject>();
        canvas = GameObject.Find("Canvas");
        arrow = GameObject.Find("Arrow");
        angle = (360 / ringCount);

        targetIndex = 0;

        GenerateTrack();
        rings[targetIndex].SetActive(true);
        turrets[targetIndex].SetActive(true);
    }
}
