using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseScreen : Screen
{
    [SerializeField] private Button muteButton;
    [SerializeField] private Button buyButton;
    public event UnityAction MuteButtonClick;
    public event UnityAction BuyButtonClick;
    public event UnityAction UnpauseButtonClick;

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
        muteButton.onClick.AddListener(OnMuteButtonClick);
        buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
        muteButton.onClick.RemoveListener(OnMuteButtonClick);
        buyButton.onClick.RemoveListener(OnBuyButtonClick);
    }
    public override void Close()
    {
        canvasGroup.blocksRaycasts = false;
        button.interactable = false;
        muteButton.interactable = false;
        buyButton.interactable = false;
        canvasGroup.alpha = 0;
    }

    public override void Open()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        button.interactable = true;
        muteButton.interactable = true;
        buyButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        UnpauseButtonClick?.Invoke();
    }

    private void OnMuteButtonClick()
    {
        MuteButtonClick?.Invoke();
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClick?.Invoke();
    }
}
