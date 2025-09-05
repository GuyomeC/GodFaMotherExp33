using UnityEngine;
using UnityEngine.InputSystem;

public class Timers : MonoBehaviour
{
    AudioManager AudioManager;
    [SerializeField] private int _timerStartValue;
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private GameObject _badEndPanel;
    [SerializeField] private PlayerInput input;
    [SerializeField] private CameraScript cam;
    private int _timer;
    private float _time;
    public int Timer { get => _timer;
        set {
            if (value < 0)
            {
                _timer = 0;
                _timerUI.UpdateTimer(Timer);
                _badEndPanel.SetActive(true);
                cam.Dezoom();
                input.DeactivateInput();
            }
            else
            {
                _timer = value;
                
                _timerUI.UpdateTimer(Timer);

            }
        }
    }

    private void Awake()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        Timer = _timerStartValue;
        _time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > 1)
        {
            Timer--;
            _time = 0;
        }

	if (Timer < 1)
	{
		AudioManager.PlaySFX(AudioManager.loseSound);
                AudioManager.musicSource.volume -= AudioManager.musicVolumeChange;
	}

        if (Timer <= 32)
        {
            AudioManager.musicSource.clip = AudioManager.clockMusic;
        }
    }
}
