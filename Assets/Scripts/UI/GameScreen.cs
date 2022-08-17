using UnityEngine.Events;

public class GameScreen : Screen
{
    public event UnityAction PauseButtonClick;
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
        PauseButtonClick?.Invoke();
    }
}
