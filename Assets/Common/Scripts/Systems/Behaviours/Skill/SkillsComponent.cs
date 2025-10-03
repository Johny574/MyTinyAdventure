using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillsComponent : Component, ISerializedComponent<SkillData[]>
{
    public Skill[] Skills = new Skill[4];
    AimComponent _aim;
    ManaComponent _mana;

    public Action<Skill[]> Updated;
    public SkillsComponent(SkillsBehaviour behaviour, Skill[] skills) : base(behaviour) {
        Skills = skills;
    }

    public void Initilize() {
        _aim = Behaviour.GetComponent<AimBehaviour>().Aim;
        _mana = Behaviour.GetComponent<ManaBehaviour>().Mana;
    }

    public void Load(SkillData[] save) {
        Skills = new Skill[4];
        for (int i = 0; i < save.Length; i++) {
            if (save[i].GUID != null) {
                SkillData skill = save[i]; // keep this reference in memory cause garbage collector clears it before the async object gets loaded
                int c = i;  // keep this reference in memory

                UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SkillSO> SkillSO = Addressables.LoadAssetAsync<SkillSO>(new AssetReference(skill.GUID));
                SkillSO.Completed += (skillso) => {
                    if (skillso.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed) {
                        throw new System.Exception($"Failed to load asset {skill.GUID}");
                    }

                    Skills[c] = new Skill(Behaviour.gameObject, skillso.Result, skill.Timer);
                    Updated.Invoke(Skills);
                };
            }
        }
    }

    public SkillData[] Save() => Skills.Select(x => x == null ? new SkillData(null, 0f) : new SkillData(x.Data.GUID, x.Timer)).ToArray();


    
    
    public void Add(SkillSO skill, GameObject caster, int slot) {
        Skills[slot] = new Skill(caster, skill);
        Updated.Invoke(Skills);
    }

    // public override void Remove<T>(T obj) {
    //     var skill = (int)(object)obj;
    //     _skills[skill] = null;
    //    GameEvents.Instance.HotbarEvents.Update?.Invoke(_skills as ISkill[]);
    // 

    public void Update() {
        for (int i = 0; i < Skills.Length; i++) {
            if (Skills[i] == null)
                return;

            Skills[i].Tick();

            if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), $"Alpha{i + 1}"))) {
                if (Skills[i].Cooldown)
                    return;

                Skills[i].Cast(_aim.AimDelta);
                _mana.Update(0 - Skills[i].Data.ManaCost);
            }
        }
    }
}