using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject loseCanvas;
    bool gameHasEnded = false;
    bool gameHasWin = false;
    public float restartDelay = 1f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && gameHasWin == true)
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameHasWin == true)
        {
            Debug.Log("QUIT");
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.T) && gameHasEnded == true)
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameHasEnded == true)
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            DeathComesForUsAll();
        }
    }

    public void WinGame()
    {
        if (gameHasWin == false)
        {
            gameHasWin = true;
            Debug.Log("Game Win");
            PleaseKillMe();

        }
    }

    void PleaseKillMe()
    {
        winCanvas.gameObject.SetActive(true);
    }

    void DeathComesForUsAll()
    {
        loseCanvas.gameObject.SetActive(true);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
