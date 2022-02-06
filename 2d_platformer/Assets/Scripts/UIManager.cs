using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //lifeLeft is for the Health class where the currentHealth is stored
    [SerializeField] private Health lifeLeft;
    //Used for the game over text
    [SerializeField] private Text _gameOverText = null;
    //Used for the vignette
    [SerializeField] private Image _vig;
    //Set the goal time for the level's bonus
    [SerializeField] private float _gameTime;
    //Checks if character is dead
    private bool isDead = false;
    private bool gameOver = false;


    //Pause Menu Object
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;

    private HighScores instance;

    //Flickers the game over screen
    //**NOT CURRENTLY IN USE**
    /*private IEnumerator GameOverTextFlickerRoutine()
    {
        isDead = true;
        while(true)
        {
            //Changes text every 0.5 seconds
            _gameOverText.text = "Game Over\n Press 'R' to return to menu";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = " ";
            yield return new WaitForSeconds(0.5f);
        }
    }*/

    private void Update() 
    {
        if(lifeLeft.currentHealth == 0)
        {
            isDead = true;
            GameOver();
        }

        //**NOT CURRENTLY IN USE**
        //Press g for the game over screen to show, along with game over flashing
        /*if(Input.GetKeyDown(KeyCode.G))
        {
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine("GameOverTextFlickerRoutine");
        }*/

        //Press r to return to the main menu
        if(Input.GetKeyDown(KeyCode.M) && (gameOver))
            {
                ScoreScript.scoreValue = 0;
                Timer.FlowingTime = 0;

                //Get Current Scene Build Index and go back to Level Select
                PlayerPrefs.SetString("PreviousLevel", SceneManager.GetActiveScene().buildIndex.ToString());
                SceneManager.LoadScene(Build.sceneOrder.LevelSelect.ToString());
            }

        //Press p to pause game
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(gameIsPaused) ResumeGame();
            else PauseGame();
        }

        if(Input.GetKeyDown(KeyCode.R) && (isDead))
            {
                lifeLeft.IncreaseHealth(3);
                RemoveText();
                isDead = false;
            }
    }

    public void RemoveText()
    {
            _gameOverText.gameObject.SetActive(false);
            isDead = false;
            gameOver = false;
    }

    public void GameOver()
    {
        //instance.AddNewScore("frog", ScoreScript.scoreValue);
        _gameOverText.gameObject.SetActive(true);
        _vig.gameObject.SetActive(true);
        gameOver = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        PlayerMovement.isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
        PlayerMovement.isPaused = true;
    }

    //Player reaches the end of the level and may get a bonus score
    public void PlayerWin()
    {
        //Takes the score value of the player
        float endScore = ScoreScript.scoreValue;
        float bonusScore = 0;
        if (Timer.FlowingTime > _gameTime) {
            //No bonus score
        }
        else {
            //bonus score compares the bonus score time minus the player's time plus 25%
            bonusScore = ((_gameTime - Timer.FlowingTime) * 1.25f);
        }
        endScore = endScore + bonusScore;
        //instance.AddNewScore("frog", (int)endScore);
        
        _gameOverText.gameObject.SetActive(true);
        _gameOverText.text = "Bonus Score: " + Mathf.Round(bonusScore) + "\nTotal Score: " + Mathf.Round(endScore) + "\n Press 'M' to return to menu";
        _vig.gameObject.SetActive(true);
        gameOver = true;
        gameIsPaused = true;
    }
}
