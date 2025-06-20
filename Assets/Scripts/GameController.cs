using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public Text gameOverScoreText;
    public Text gameOverMessageText;

    [Header("Game State")]
    public bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverMessageText != null) gameOverMessageText.text = "GAME OVER!";
        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = "Điểm của bạn: " + FindObjectOfType<PlayerController>().score.ToString();
        }

        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
