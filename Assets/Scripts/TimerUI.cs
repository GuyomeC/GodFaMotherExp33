using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Text timerText;

    private void UpdateTimer(int value)
    {
        timerText.text = value / 60 + ":" + value % 60;
    }
}
