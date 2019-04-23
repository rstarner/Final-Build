using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text WinText;
    public AudioClip winSound;
    public AudioClip loseSound;
   
   
    private bool gameOver;
    private bool Restart;
    private bool Win;
    private int score;
    private AudioSource audiosource;
    internal bool gameover;


    void Start()
    {
        gameOver = false;
        Restart = false;
        Win = false;
        RestartText.text = "";
        GameOverText.text = "";
        WinText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        audiosource = GetComponent<AudioSource>();

    }

    void Update() {


        if (Restart)

           
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
               
                SceneManager.LoadScene("SCENE2");
            }

            if (Input.GetKey("escape"))
                Application.Quit();

        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                audiosource.clip = loseSound;
                audiosource.Play();
                RestartText.text = "Press 'Q' for Restart";
                Restart = true;
                break;
            }
            if (Win)
            {
                audiosource.clip = winSound;
                audiosource.Play();
                RestartText.text = "Press 'Q' for Restart";
                Restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            audiosource.clip = winSound;
            audiosource.Play();
            WinText.text = "You win!";
            gameOver = true;
            Restart = true;
        }
    }

    public void GameOver()
    {
        audiosource.clip = loseSound;
        audiosource.Play();
        GameOverText.text = "GAME CREATED BY ROMAN STARNER";
        gameOver = true;
    }
}