using UnityEngine;
using UnityEngine.UI;

public class ScreenCredits : MonoBehaviour, IScreen
{
    bool _active;
    public Button buttonBack;

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
        buttonBack.interactable = true;
    }
}
