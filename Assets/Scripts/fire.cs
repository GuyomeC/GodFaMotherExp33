using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] AudioManager AudioManager;

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void GoodGuess()
    {
        AudioManager.PlaySFX(AudioManager.findCatSound);
    }

    public void BadGuess()
    {
        AudioManager.PlaySFX(AudioManager.nobodyInCrosshairSound);
    }

    public void Coin()
    {
        AudioManager.PlaySFX(AudioManager.FindCoinSound);
    }
}
