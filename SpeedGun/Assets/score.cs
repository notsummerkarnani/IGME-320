using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class score : MonoBehaviour
{
    public float lapTime;
    float fastLapTime;
    int numPenalties;
    Text scoreText;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lapTime = 0.0f;
        fastLapTime = 0.0f;
        numPenalties = 0;
        player = GameObject.Find("Drone");

        scoreText = gameObject.GetComponentInChildren<Text>();
        if (scoreText)
            scoreText.text = $"Lap time: {(int)lapTime}\nLast lap time: {fastLapTime.ToString("F1")}\nPenalties: {numPenalties}";
    }

    // Update is called once per frame
    void Update()
    {
        if(lapTime != 0)
        {
            lapTime += Time.deltaTime;
        }
        if (player.GetComponent<Movement>().cooldown)
        {
            scoreText.text = $"Lap time: {(int)lapTime}\nFastest lap time: {fastLapTime.ToString("F1")}\nPenalties: {numPenalties}\nRELOADING";
            return;
        }
        scoreText.text = $"Lap time: {(int)lapTime}\nFastest lap time: {fastLapTime.ToString("F1")}\nPenalties: {numPenalties}";
    }

    public void StartClock()
    {
        lapTime += Time.deltaTime;
        numPenalties = 0;
    }

    public void StopClock()
    {
        if(lapTime<=fastLapTime || fastLapTime == 0)
            fastLapTime = lapTime;
        
        lapTime = 0;
    }

    public void AddPenalty()
    {
        if (lapTime != 0)
        {
            lapTime += 2f;
            numPenalties++; 
        }
    }

}

