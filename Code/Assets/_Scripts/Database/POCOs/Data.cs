using System;
using Newtonsoft.Json;

public abstract class Data
{
    public event Action ValueChanged;

    protected void OnValueChanged()
    {
        ValueChanged?.Invoke();
    }
}