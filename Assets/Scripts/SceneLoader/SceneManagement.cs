using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    public static int ReturnThisSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(int level)
    {
        JsonManager.instance.Save();
        SceneManager.LoadScene(level);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(JsonManager.instance.data.sceneToLoad);
    }

    public void LoadLastScene()
    {
        SceneManager.LoadScene(JsonManager.instance.data.lastScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
