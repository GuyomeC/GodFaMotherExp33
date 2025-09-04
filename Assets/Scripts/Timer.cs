using UnityEngine;

public class Timers : MonoBehaviour
{
    [SerializeField] private int _timerStartValue;
    [SerializeField] private TimerUI _timerUI;
    private int _timer;
    private float _time;
    public int Timer { get => _timer;
        set {
            if (value < 0)
            {
                _timer = 0;
                _timerUI.UpdateTimer(Timer);
                //endGame
            }
            else
            {
                _timer = value;
                _timerUI.UpdateTimer(Timer);

            }
        }
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
    }
}
