
using System.Collections.Generic;
using UnityEngine;

// namespace SkillCommands
// {
//     public class CastCommand : ICommand {
//         private Skill _skill;
//         private Vector2 _direction;
//         private ManaComponent _mana;

//         public CastCommand(Skill skill, Vector2 direction, ManaComponent mana) {
//             _skill = skill;
//             _direction = direction;
//             _mana = mana;
//         }

//         public void Execute() {
//             if (_mana.Get<Counter>().Count < _skill.Data.ManaCost) {
//                 Debug.Log("No mana for skill");
//                 return;
//             }
//             else if (_skill.Cooldown) {
//                 Debug.Log("Skill on cooldown");
//                 return;
//             }

//             _mana.Remove((int)(0 - _skill.Data.ManaCost));
//             _skill.Cast(_direction);
//         }
//     }
//     public class EquiptCommand : ICommand {
//         private SkillData skill;
//         int slot;
//         private EntityComponent entity;

//         public EquiptCommand(SkillData skill, int slot, EntityComponent entity) {
//             this.skill = skill;
//             this.slot = slot;
//             this.entity = entity;
//         }

//         public void Execute() {

//             entity.Service<SkillComponent>().Add(new KeyValuePair<int, Skill>(slot, ResourceFactory.Skills[skill.GetType()].Invoke(skill, entity)));
//         }
//     }
// }