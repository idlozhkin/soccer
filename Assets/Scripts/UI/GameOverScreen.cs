using UnityEngine;
using UnityEngine.Events;

public class GameOverScreen : Screen
{
    public event UnityAction RestartButtonClick;
    public override void Close()
    {
        canvasGroup.blocksRaycasts = false;
        button.interactable = false;
        canvasGroup.alpha = 0;
    }

    public override void Open()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        button.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
    }
}
