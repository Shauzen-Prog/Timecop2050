using System.Collections.Generic;
public static class EventManager
{
    static Dictionary<string, EventReceiver> call = new Dictionary<string, EventReceiver>();

    public delegate void EventReceiver(params object[] parameters);

    public static void Suscribe(string eventType, EventReceiver function)
    {
        if (call.ContainsKey(eventType))
            call[eventType] += function;
        else
            call.Add(eventType, function);
    }

    public static void UnSuscribe(string eventType, EventReceiver function)
    {
        if (call.ContainsKey(eventType))
        {
            call[eventType] -= function;

            if (call[eventType] == null)
            {
                call.Remove(eventType);
            }
        }
    }

    public static void Trigger(string eventType, params object[] parameters)
    {
        if (call.ContainsKey(eventType))
        {
            call[eventType](parameters);
        }
    }
}

