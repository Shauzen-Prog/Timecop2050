using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    
    Stack<IScreen> _screens = new Stack<IScreen>();

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    public void Push(IScreen screen)
    {
        if (_screens.Count > 0)
            _screens.Peek().Desactivate();

        _screens.Push(screen);
        screen.Activate();
    }

    public void Push(string name)
    {
        var takeGameObject = Instantiate(Resources.Load<GameObject>(name));

        Push(takeGameObject.GetComponent<IScreen>());
    }

    public void Pop()
    {
        if (_screens.Count > 0)
            _screens.Pop().Free();

        if (_screens.Count > 0)
            _screens.Peek().Activate();
    }
}
