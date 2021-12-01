using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{
    static public float totalScore;
    static public int bottlesHeld;
    static public int deposits;
    static public int highestDeposit;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0.0f;
        bottlesHeld = 0;
        deposits = 0;
        highestDeposit = 0;
        scoreText = gameObject.GetComponentInChildren<Text>();
        if (scoreText)
            scoreText.text = $"Score: {(int)totalScore}\nBottles held: {bottlesHeld}\nDeposits: {deposits}";
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {   
        if(SceneManager.GetActiveScene().buildIndex == 2 && GameObject.Find("Canvas"))
        {
            GameObject.Find("Text (1)").GetComponent<Text>().text = $"Bottles deposited: {deposits}\nHighest deposit: {highestDeposit}\nFinal score: {(int)totalScore}";
            Destroy(GameObject.Find("Canvas"));
            Destroy(this);
        }

        totalScore += Time.deltaTime * 2;
        this.scoreText.text = $"Score: {(int)totalScore}\nBottles held: {bottlesHeld}\nDeposits: {deposits}";

        
    }

    static public void Deposit()
    {
        if (bottlesHeld > highestDeposit)
        {
            highestDeposit = bottlesHeld;
        }
        totalScore += Mathf.Pow(bottlesHeld,2);
        deposits += bottlesHeld;
        bottlesHeld = 0;
    }

    static public void PickUp()
    {
        bottlesHeld++;
    }
}
