public interface IObservable 
{
    /// <summary>
    /// El observador debe subscribirse a este evento para poder observar
    /// </summary>
    /// <param name="obs"></param>
    void Subscribe(IObserver obs);
    /// <summary>
    /// El observador puede usarlo para dejar de observar 
    /// </summary>
    /// <param name="obs"></param>
    void UnSubscribe(IObserver obs);
    /// <summary>
    /// Notifica a los observadores 
    /// </summary>
    /// <param name="eventEnum"></param>
    /// <param name="life"></param>
    void NotifyToObservers(EventEnum eventEnum,params object[] parameters);
}
public enum EventEnum
{
    TakeDamage,
    Death,
    Healing
}
