using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private int _score;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public Canvas startMenu, pauseMenu, optionsMenu, gameOverMenu;
    public bool isGameActive = false;

    // Update is called once per frame
    private void Start()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    void Update()
    { // Pauses and unpauses the game
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        } 
        else if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }

        if (isGameActive == true)
        {
            Cursor.visible = false;
        }
        else 
        {
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        // Starts the game
        isGameActive = true;

        _score = 0;
        UpdateScore(0);
        // Deactivates the title screen
        startMenu.gameObject.SetActive(false);
    }

    public void UpdateScore(int scoreToAdd)
    {
        // Updates the score when called by the passed in int variable
        _score += scoreToAdd;
        scoreText.text = "Score: " + _score;

        if (_score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            highScoreText.text = "High Score: " + _score;
        }       
    }

    public void GameOver()
    {
        // Pauses the game and brings up the game over menu when called.
        gameOverMenu.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {   // Restarts the current scene when callled. Rquires "Using UnityEngine.SceneManager"
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
        isGameActive = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void OptionsMenu()
    {
        startMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void StartMenu()
    {
        startMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
        isGameActive = false;
    }
}
