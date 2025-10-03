using System;
using UnityEngine;

public class HealthComponent : StatComponent, ISerializedComponent<BarData> {
    public Action Death; 
    StatpointsComponent _stats;
    FeedbackComponent _feedback;
    ParticleSystem _bloodparticles;
    IPoolObject<BarData> _healthbar;
    public bool Dead = false;

    public HealthComponent(HealthBehaviour behaviour, ParticleSystem bloodparticles) : base(behaviour) {
        _bloodparticles = bloodparticles;
    }

    public void Initilize() {
        _feedback = new(); 
        _stats = Behaviour.GetComponent<StatpointsBehaviour>().Stats;
        Data = new BarData(_stats.HPPool, _stats.HPPool);
        Data.CalculateFill();
        CreateHealthBar();
        Changed?.Invoke(Data);
        Changed += async (bardata) => await _feedback.DisplayFeedback(new($"{bardata.Amount}", Color.red), Behaviour.transform.position);
    }

    async void CreateHealthBar() {
       _healthbar = await HealthbarFactory.Instance.Pool.GetObject<BarData>();
        _healthbar.Bind(Data);
        ((MonoBehaviour)_healthbar).GetComponent<Follower>().Follow(Behaviour.gameObject);
        Changed += _healthbar.Bind;
    }

    public void Die() {
        ((MonoBehaviour)_healthbar).gameObject.SetActive(false);
        Behaviour.gameObject.SetActive(false);
        Dead = true;
    }

    public void Update(int amount) {
        Data.Amount += amount;
        Data.Amount = Mathf.Clamp(Data.Amount, 0, _stats.HPPool);
        if (Mathf.Sign(amount) < 0) {
            _bloodparticles.Emit(30);
        }

        Data.CalculateFill();
        Changed?.Invoke(Data);
        // _healthbar.Bind(Data);
        
        if (Data.Amount <= 0)
            Death.Invoke();
    }

    public BarData Save() {
        return Data;
    }

    public void Load(BarData save) {
        Data = save;
        Changed?.Invoke(Data);
        Dead = false;
    }
}