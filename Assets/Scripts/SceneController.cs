using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static int _gameSceneIndex = 1;
    private static int _pauseSceneIndex = 2;
    private static int _endSceneIndex = 3;

    public static void Begin()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_gameSceneIndex);
    }

    public static void Pause()
    {
        SceneManager.LoadScene(_pauseSceneIndex, LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public static void Continue()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(_pauseSceneIndex);
    }

    public static void GameOver()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(_endSceneIndex);
    }

    public static void Exit()
    {
        Application.Quit();
    }
}
