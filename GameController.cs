using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioClip musicClipThree;

    public AudioSource musicSource;

    public GameObject[] hazards;
    public GameObject powerupPrefab;
    public GameObject powerupPrefab2;


    public Vector3 spawnValues;
    public int hazardCount;
    public int powerupCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int nonpowerup;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    public int score;
    private bool init;
    private bool flip;
     

    void Start()
    {
        musicSource.clip = musicClipOne;
        musicSource.Play();
        gameOver = false;
        restart = false;
        init = true;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        nonpowerup = 3;
        flip = false;
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("thisisfinalpart1withmusic");
            }
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
                if (nonpowerup > 5)
                {
                    nonpowerup = 0;
                    if (flip == true)
                    {
                        Instantiate(powerupPrefab, spawnPosition, spawnRotation);
                        flip = false;
                    }
                    else
                    {
                        Instantiate(powerupPrefab2, spawnPosition, spawnRotation);
                        flip = true;
                    }
                }
                else
                {
                    nonpowerup += 1;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                }

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Y' for Restart";
                restart = true;
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
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            gameOverText.text = "You win! Game Created by Amelia Stephens!";
            gameOver = true;
            restart = true;
            Destroy(GetComponent<Rigidbody2D>());
            musicSource.clip = musicClipTwo;
            musicSource.Play();


        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = musicClipThree;
        musicSource.Play();
    }
}
