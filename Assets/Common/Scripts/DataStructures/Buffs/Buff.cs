using System;
using UnityEngine;

[Serializable]
public abstract class Buff  {
    public abstract BuffSO SO { get; set; }
    private GameObject _user;
    public Action<Buff> Finished;

    public BuffData Data;
    int _currentTimeAsInt = 0;

    public Buff(BuffSO so, GameObject user) {
        _user = user;
        SO = so;
        Data = new BuffData(0, so.GUID);
        Finished += (buff) => OnFinished(_user);
        OnAdded(user);
    }

    public void Tick() {
        if (Data.Timer < SO.Duration) {
            Data.Tick();
            int counterTimeAsInt = Mathf.FloorToInt(Data.Timer);

            if (_currentTimeAsInt != counterTimeAsInt) {
                OnTick(_user, Data.Timer);
                _currentTimeAsInt = counterTimeAsInt;
            }
        }

        else {
            Finished?.Invoke(this);
        }
    }

    public abstract void OnTick(GameObject user, float timer);
    public abstract void OnAdded(GameObject user);
    public abstract void OnFinished(GameObject user);
}