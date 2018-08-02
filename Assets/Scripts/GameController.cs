using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Camera cam;
    public GameObject [] balls;
    public float timeLeft;
    public Text timerText;
    private float maxWidth;
    public GameObject gameOverText;
    public GameObject restartButton;

    
    public GameObject splashScreen;
    public GameObject startButton;
    public GameObject startOne;
    public GameObject startTwo;

    public HatController hatController;

    private bool playing;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        playing = false;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = balls[0].GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;
        
        
        UpdateText();
    }

    void FixedUpdate()
    {
        if (playing)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
            UpdateText();
        }
    }
    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        startOne.SetActive(true);

        
        StartCoroutine(Change());
        hatController.ToggleControl(true);
        //to start our function our game
        StartCoroutine(Spawn());
    }
    IEnumerator Change()
    {
        yield return new WaitForSeconds(0.1f);
        startOne.SetActive(false);
        startTwo.SetActive(true);

        yield return new WaitForSeconds(0.1f);
      

        startTwo.SetActive(false);


    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);
        playing = true;
        while (timeLeft > 0)
        {
            GameObject ball = balls[Random.Range (0 , balls.Length)];
            //random ball
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0.0f);
            //for no retation
            Quaternion spawnRetation = Quaternion.identity;
            Instantiate(ball, spawnPosition, spawnRetation);
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
        yield return new WaitForSeconds(2.0f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        restartButton.SetActive(true);
    }
    void UpdateText()
    {
        timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
    }
}
