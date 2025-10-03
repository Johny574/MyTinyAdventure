using System;
using UnityEngine;

public class CurrencyComponent : Component, ISerializedComponent<Currency>
{
    public Currency Currency = new(0);
    public Action<int> Changed;
    FeedbackComponent _feedback;

    public CurrencyComponent(CurrencyBehaviour behaviour, int amount) : base(behaviour) {
        Currency = new(amount);
        _feedback = new();
        Behaviour.GetComponent<HealthBehaviour>().Health.Death += OnDeath;
    }

    public void Initilize() {
        Changed += async (amount) => await _feedback.DisplayFeedback(new FeedbackData($"{amount}", Color.yellow), Behaviour.transform.position);
        Currency = new(0);
    }

    public void Add(int amount) {
        Currency.Add(amount);
        Changed?.Invoke(amount);
    }

    public void Load(Currency save) => Currency = save;
    public Currency Save() => Currency;

    public void Remove(int amount) {
        Currency.Remove(amount);
        Changed?.Invoke(amount);
    }

    void OnDeath() {
        if (Currency.TotalCopper == 0)
            return;

        int numberOfCoins = UnityEngine.Random.Range(3, 6);  

        if (Currency.TotalCopper < numberOfCoins) 
            new CurrencyCommands.DropCommand(Currency.TotalCopper, Behaviour.transform.position).Execute();
        else {
            for (int i = 0; i < numberOfCoins; i++) 
                new CurrencyCommands.DropCommand(Currency.TotalCopper / numberOfCoins, Behaviour.transform.position).Execute();

            new CurrencyCommands.DropCommand(Currency.TotalCopper % numberOfCoins, Behaviour.transform.position).Execute();
        }
    }
}

public class CurrencyException : Exception {
    public CurrencyException(string message) : base(message) { 

    }
}