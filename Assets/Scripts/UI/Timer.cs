using UnityEngine;
using TMPro;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    private TMP_Text timeText;
    private float time;

    private void Start()
    {
        timeText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        gameManager.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        gameManager.TimeChanged -= OnTimeChanged;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        OnTimeChanged(time);
    }

    private void OnTimeChanged(float time)
    {
        this.time = Mathf.Max(time, 0);
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
