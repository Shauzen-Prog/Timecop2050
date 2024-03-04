public interface IObserver
{
    /// <summary>
    /// Recibe los datos de sus Observados
    /// </summary>
    /// <param name="eventEnum"></param>
    /// <param name="life"></param>
    void Notify(EventEnum eventEnum, params object[] parameters);
}
