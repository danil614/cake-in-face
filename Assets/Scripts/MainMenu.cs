using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    ///     Запустить сцену с игрой.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    ///     Запустить стартовую сцену.
    /// </summary>
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}