using UnityEngine;
using TMPro;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class Coins : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    private TMP_Text coinsText;

    private void Start()
    {
        coinsText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        gameManager.CoinsChanged += OnCoinsChanged;
    }

    private void OnDisable()
    {
        gameManager.CoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        coinsText.text = coins.ToString();
    }
}
