using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private string minText;
    private string secText;

    public void UpdateTimer(int value)
    {
        int min = value / 60;
        if(min<10)
        {
            minText = "0" + min;
        }
        else
        {
            minText = ""+min;
        }
        int sec = value % 60;
        if (sec < 10)
        {
            secText = "0" + sec;
        }
        else
        {
            secText = "" + sec;
        }
        timerText.text = minText + ":" + secText;
    }
}
