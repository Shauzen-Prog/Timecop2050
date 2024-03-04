using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class PlayerView : MonoBehaviour, IObserver
{
    public Slider sliderBar;
    private IObservable _myModel;
    public void Start()
    {
        _myModel = GetComponent<Entity>();
        _myModel.Subscribe(this);
        
        sliderBar.value = PlayerModel.instance.actualHealth;
    }
    
    private void UpdateHealBar(float health)
    {
        sliderBar.value = health / 100f;
    }
    
    public void Notify(EventEnum eventEnum, params object[] parameters)
    {
        var healAmount = (float)parameters[0];
        
        switch (eventEnum)
        {
            case EventEnum.TakeDamage:
            case EventEnum.Healing:
                UpdateHealBar(healAmount);
                break;
        }
    }

    private void OnDisable()
    {
        _myModel.UnSubscribe(this);
    }
}
