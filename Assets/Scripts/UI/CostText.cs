using UnityEngine;
using TMPro;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class CostText : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    private TMP_Text costText;

    private void Start()
    {
        costText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        gameManager.CostChanged += OnCostChanged;
    }

    private void OnDisable()
    {
        gameManager.CostChanged -= OnCostChanged;
    }

    private void OnCostChanged(int cost)
    {
        costText.text = $"{cost} coins";
    }
}
