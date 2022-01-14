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
    //Checks if character is dead
    private bool isDead = false;
    private bool gameOver = false;

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
        if(Input.GetKeyDown(KeyCode.R) && (isDead || gameOver))
            {
                ScoreScript.scoreValue = 0;
                Timer.FlowingTime = 0;
                SceneManager.LoadScene("MainMenu");
            }
    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _vig.gameObject.SetActive(true);
        gameOver = true;
    }


    //private Timer _FlowingTime;
    public void PlayerWin()
    {
        float endScore = ScoreScript.scoreValue;
        float bonusScore;
        if (Timer.FlowingTime > 30f) {
            bonusScore = 0;
        }
        else {
            bonusScore = (Timer.FlowingTime * 1.1f);
        }
        endScore = endScore + bonusScore;
        _gameOverText.gameObject.SetActive(true);
        _gameOverText.text = "You Win!\n" + "Final Score: " + Mathf.Round(endScore) + "\n Press 'R' to return to menu";
        _vig.gameObject.SetActive(true);
        gameOver = true;
    }
}