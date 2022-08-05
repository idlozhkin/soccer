using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private float time;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        timeText = GetComponent<TextMeshProUGUI>();
        SetTime();
    }

    private void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if (time >= 0)
        {
            SetTimerText();
        }
        else
        {
            gameManager.StartLevel();
        }
    }

    private void SetTimerText()
    {
        timeText.text = (int)time / 60 + ":" + (int)(time % 60);
    }

    public void SetTime()
    {
        time = GameBalance.GetRoundDuration();
    }
}
