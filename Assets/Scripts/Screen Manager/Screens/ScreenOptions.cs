using UnityEngine;
using UnityEngine.UI;


public class ScreenOptions : MonoBehaviour, IScreen
{
    bool _active;
    public Button buttonSound, buttonCredits, buttonBack;

    public void BTN_Sound()
    {
        if (!_active) return;

        ScreenManager.instance.Push(ScreenNameEnum.ScreenSound.ToString());
    }

    public void BTN_Credits()
    {
        if (!_active) return;

        ScreenManager.instance.Push(ScreenNameEnum.ScreenCredits.ToString());
    }
    public void BTN_Back()
    {
        if (!_active) return;

        ScreenManager.instance.Pop();
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
        buttonSound.interactable = true;
        buttonCredits.interactable = true;
        buttonBack.interactable = true;
    }
}
