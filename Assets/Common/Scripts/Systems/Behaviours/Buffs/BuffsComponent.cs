using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;

public class BuffComponent : Component, ISerializedComponent<List<BuffData>> {
    
    Dictionary<BuffSO, Buff> _buffs;            // using hashset so that buffs dont stack

    public BuffComponent(BuffsBehaviour behaviour) : base(behaviour) {
    }

    public void Add(BuffSO data) {

        if (data == null || _buffs.ContainsKey(data))
            return;

        Buff buff = BuffFactory.Buffs[data.GetType()].Invoke(data, Behaviour.gameObject);
        _buffs.Add(data, buff);
        buff.Finished += (buff) => Remove(buff.SO);
    }

    public void Remove(BuffSO buff) {
        if (!_buffs.ContainsKey(buff)) 
            return;

        _buffs[buff].Finished -= (buff) => Remove(buff.SO);
        _buffs.Remove(buff);
    }

    public void Tick() {
        foreach (var key in _buffs.Keys.ToList())
            _buffs[key].Tick();

        // List<IBuffSource> _buffCollisions = Entity?.Service<CollisionComponent>().Get<List<IBuffSource>>();
        // for (int i = 0; i < _buffCollisions?.Count; i++) {
        //     List<BuffData> _buffs = _buffCollisions[i].Buffs();

        //     for (int b = 0; b < _buffs.Count; b++) {
        //         if (!Buffs.ContainsKey(_buffs[b].ID)) {
        //             Buffs.Add(_buffs[b].ID, ResourceFactory.Buffs[_buffs[b].GetType()].Invoke(_buffs[b], Entity));
        //         }
        //     }
        // }
    }

    public void Initilize() => _buffs = new();
    public List<BuffData> Save() => _buffs.Select(x => x.Value.Data).ToList();

    public void Load(List<BuffData> save) {
        _buffs = new();
        foreach (var buffdata in save) {
            UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<BuffSO> buffSO = Addressables.LoadAssetAsync<BuffSO>(new AssetReference(buffdata.GUID));
            buffSO.Completed += (buffso) => {
                if (buffso.Status.Equals(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed))
                    throw new System.Exception($"Failed to load asset {buffdata.GUID}");

                Buff buff = BuffFactory.Buffs[buffso.Result.GetType()].Invoke(buffso.Result, Behaviour.gameObject);
                buff.Data.Timer = buffdata.Timer;
                _buffs.Add(buffso.Result, buff);
                buffso.Release();
            };
        }
    }

}