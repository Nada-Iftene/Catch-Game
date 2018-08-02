using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    public Text scoreText;
    public int ballValue;
    private int score;

    // Use this for initialization
    void Start () {
        score = 0;
        UpdateScore();

    }
    void OnTriggerEnter2D()
    {
        score += ballValue;
        UpdateScore();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "Bomb")
        {
            score -= ballValue * 2;
            UpdateScore();
        }
        
    }
	
	// Update is called once per frame
	void UpdateScore () {
        if(score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score:\n" + score;

    }
}
