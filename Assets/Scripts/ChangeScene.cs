using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
