using System.Collections.Generic;

public class LookUpTable<T1,T2>
{
    public delegate T2 FactoryMethod(T1 key);

    Dictionary<T1, T2> _table = new Dictionary<T1, T2>();

    FactoryMethod _factoryMethod;

    public LookUpTable(FactoryMethod newFactory)
    {
        _factoryMethod = newFactory;
    }

    public T2 ReturnValue(T1 key)
    {
        if(!_table.ContainsKey(key))
        {
            var value = _factoryMethod(key);
            _table.Add(key, value);
            return value;
        }

        return _table[key];
    }
}
