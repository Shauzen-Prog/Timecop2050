using UnityEngine;

public abstract class GenericScreen : MonoBehaviour, IScreen
{
    bool _active;

    public virtual void BTN_Return()
    {
        ScreenManager.instance.Pop();
    }

    public abstract void Activate();

    public abstract void Desactivate();

    public virtual void Free()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    /*
    public virtual void InteractableButtons()
    {

    }
    */
    
}
