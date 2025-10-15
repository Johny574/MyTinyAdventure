using System;
using UnityEngine;

public class HealthComponent : StatComponent, ISerializedComponent<BarData> {
    public Action Death; 
    StatpointsComponent _stats;
    ParticleSystem _bloodparticles;
    IPoolObject<BarData> _healthbar;
    public bool Dead = false;

    public HealthComponent(HealthBehaviour behaviour, ParticleSystem bloodparticles) : base(behaviour) {
        _bloodparticles = bloodparticles;
    }

    public void Initilize() {
        _stats = Behaviour.GetComponent<StatpointsBehaviour>().Stats;
        Data = new BarData(_stats.HPPool, _stats.HPPool);
        Data.CalculateFill();
        CreateHealthBar();
        Changed?.Invoke(Data, 0);
        Changed += async (bardata, amount) =>
        {
            if (amount == 0 || bardata.Amount > bardata.Max)
                return;
                
            await FeedbackFactory.Instance.Display(new($"{amount}", Color.red), Behaviour.transform.position);
        };
    }

    async void CreateHealthBar() => _healthbar = await HealthbarFactory.Instance.CreateHealthBar(Data, this);

    public void Die() {
        ((MonoBehaviour)_healthbar).gameObject.SetActive(false);
        Behaviour.gameObject.SetActive(false);
        Dead = true;
    }

    public void Update(float amount) {
        Data.Amount += amount;
        Data.Amount = Mathf.Clamp(Data.Amount, 0, _stats.HPPool);
        if (Mathf.Sign(amount) < 0) {
            _bloodparticles.Emit(30);
        }

        Data.CalculateFill();
        Changed?.Invoke(Data, amount);
        // _healthbar.Bind(Data);
        
        if (Data.Amount <= 0)
            Death.Invoke();
    }

    public BarData Save() => Data;

    public void Load(BarData save) {
        Data = save;
        Changed?.Invoke(Data, 0);
        Dead = false;
    }
}