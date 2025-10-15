using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffFactory : MonoBehaviour
{
    public static Dictionary<Type, Func<BuffSO, GameObject, Buff>> Buffs = new()
    {
        { typeof(DamageBuffData),           (data, user) => new DamageBuff(data, user)},
        { typeof(HealBuffData),             (data, user) => new HealBuff(data, user)},
        { typeof(ShieldBuffData),           (data, user) => new ShieldBuff(data, user)},
        { typeof(SpeedBuffData),            (data, user) => new SpeedBuff(data, user)},
        { typeof(StunBuffData),             (data, user) => new StunBuff(data, user)},
    };
}