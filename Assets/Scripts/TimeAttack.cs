using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour
{
    private GameController gameController;

    public int timeLimit = 1;
    public float timeLeft;
    public Text timeText;
    public AudioClip loseSound;
    public bool TimerWork = true;

    private float seconds;
    private AudioSource audiosource;

    void Awake()
    {
        timeLeft = 30f;
    }

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        audiosource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (timeLeft <= 0)
        {
            TimerWork = false;
            gameController.gameover = true;
            gameController.GameOver();
            audiosource.clip = loseSound;
            audiosource.Play();
        }
        if (timeLeft >= 0 && TimerWork == true)
            timeLeft -= Time.deltaTime;
        UpdateTime();
    }

    public void UpdateTime()
    {
        seconds = timeLeft % 30;

        timeText.text = "Time Left: " + seconds;
    }
}