using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField]private GameObject gameOverScreen;
    [SerializeField]private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else 
                PauseGame(true);
        }
     }

 #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2/*SceneManager.GetActiveScene().buildIndex*/);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    #endregion

    #region Pause
    public void PauseMenu()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if(status)
          Time.timeScale = 0;
        else
         Time.timeScale=1;
    }

    public void SoundValue()
    {
        SoundManager.instance.ChangeSoundValue(0.2f);
    }

    public void MusicValue()
    {
        SoundManager.instance.ChangeMusicValue(0.2f);
    }

    #endregion
}