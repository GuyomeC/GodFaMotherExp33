using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    AudioManager AudioManager;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void ChangeToScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        AudioManager.musicSource.volume = AudioManager.musicVolumeBase;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
