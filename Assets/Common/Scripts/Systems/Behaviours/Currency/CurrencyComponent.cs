using System;
using UnityEngine;

public class CurrencyComponent : Component, ISerializedComponent<Currency>
{
    public Currency Currency = new(0);
    public Action<int> Changed;

    public CurrencyComponent(CurrencyBehaviour behaviour, int amount) : base(behaviour) {
        Currency = new(amount);
        Behaviour.GetComponent<HealthBehaviour>().Health.Death += OnDeath;
    }

    public void Initilize() {
        Changed += async (amount) => await FeedbackFactory.Instance.Display(new FeedbackData($"{amount}", Color.yellow), Behaviour.transform.position);
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

        // todo coin modifier 
        int numberOfCoins = UnityEngine.Random.Range(3, 6);  

        if (Currency.TotalCopper < numberOfCoins) 
            CoinFactory.Instance.Drop(Behaviour.transform.position, Currency.TotalCopper);
        else {
            for (int i = 0; i < numberOfCoins; i++) 
                CoinFactory.Instance.Drop(Behaviour.transform.position, Currency.TotalCopper / numberOfCoins);

            CoinFactory.Instance.Drop(Behaviour.transform.position, Currency.TotalCopper % numberOfCoins);
        }
    }
}

public class CurrencyException : Exception {
    public CurrencyException(string message) : base(message) { 

    }
}