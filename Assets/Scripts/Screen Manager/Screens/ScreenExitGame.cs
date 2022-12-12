using UnityEngine;
using UnityEngine.UI;

public class ScreenExitGame : GenericScreen, IScreen
{
    bool _active;
    public Button buttonYes, buttonNo;
    
    public void BTN_Yes()
    {
        if (!_active) return;

        //ScreenManager.instance.Push("QuitTheGame"); Cambiar a SceneManager el quit
    }
    public void BTN_No()
    {
        if (!_active) return;

        ScreenManager.instance.Pop();
    }
    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Desactivate()
    {
        throw new System.NotImplementedException();
    }

    void InteractableButtons()
    {
        buttonYes.interactable = true;
        buttonNo.interactable = true;
    }

}
