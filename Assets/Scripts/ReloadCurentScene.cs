using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadCurrentScene : MonoBehaviour
{
    public void ReloadScene()
    {
        if (GameController.instance != null)
        {
            if (GameController.instance.isGameOver)
            {
                Time.timeScale = 1;
                GameController.instance.isGameOver = false;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}