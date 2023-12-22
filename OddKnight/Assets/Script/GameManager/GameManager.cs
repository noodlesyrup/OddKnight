using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    bool lvlComplete = false;
    public GameObject gameOverUI;
    public GameObject lvlCompleteUI;
    public GameObject soundManager;

    public void EndGmae()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Invoke("GameOverUI", 2.5f);
            
        }
    }
    public void LvlComplete()
    {
        if (lvlComplete == false)
        {
            lvlComplete = true;
            Invoke("LvlCompletion", 2.5f);
            
        }
    }
    void GameOverUI()
    {
        gameOverUI.SetActive(true);
        soundManager.SetActive(false);
    }
    void LvlCompletion()
    {
        lvlCompleteUI.SetActive(true);
        soundManager.SetActive(false);
    }

    public void NextLvl()
    {
        SceneManager.LoadScene("Lvl2");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
