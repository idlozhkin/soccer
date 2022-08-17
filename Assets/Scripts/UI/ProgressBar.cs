using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        gameManager.ProgressChanged += OnProgressChanged;
    }

    private void OnDisable()
    {
        gameManager.ProgressChanged -= OnProgressChanged;
    }

    private void OnProgressChanged(int value, int maxValue)
    {
        slider.value = (float)value / maxValue;
    }
}
