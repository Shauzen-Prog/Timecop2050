using UnityEngine;
using UnityEngine.UI;

public enum ScreenNameEnum
{
    ScreenOptions,
    ScreenExitGame,
    ScreenSound,
    ScreenCredits
}
public class ScreenPause : MonoBehaviour, IScreen
{
   
    bool _active;
    public Button buttonBack, buttonConfig, buttonExit;
    public void BTN_Return()
    {
        if (!_active) return;

        ScreenManager.instance.Pop();
    }

    public void BTN_Options()
    {
        if (!_active) return;

        ScreenManager.instance.Push(ScreenNameEnum.ScreenOptions.ToString());
    }

    public void BTN_ExitGame()
    {
        if (!_active) return;

        ScreenManager.instance.Push(ScreenNameEnum.ScreenExitGame.ToString());
    }

    public void Activate()
    {
        _active = true;
        InteractableButtons();
    }

    public void Desactivate()
    {
        _active = false;
        InteractableButtons();
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    void InteractableButtons()
    {
        buttonBack.interactable = true;
        buttonConfig.interactable = true;
        buttonExit.interactable = true;
    }
}
