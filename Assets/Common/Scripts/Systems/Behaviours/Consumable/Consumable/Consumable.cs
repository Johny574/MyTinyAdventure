using UnityEngine;

public class Consumable
{
    public ItemStack Stack;
    public float Timer { get; private set; }
    public bool OnCooldown = false;
    public float Fill;

    public Consumable(ItemStack stack, float timer = 0f) {
        Stack = stack;
        Timer = timer;
    }

    public void Tick() {
        if (!OnCooldown || Stack.Item == null || Stack.Count == 0)
            return;

        if (Timer < (Stack.Item as ConsumableSO).CooldownDuration) {
            Timer += Time.deltaTime;
            Fill = 1 - Timer / ((ConsumableSO)Stack.Item).CooldownDuration ;
        }

        else {
            OnCooldown = false;
            Timer = 0f;
            Fill = Timer / ((ConsumableSO)Stack.Item).CooldownDuration ;
        }
    }

    public void Consume(ConsumableComponent consumer) {
        consumer.Behaviour.GetComponent<BuffsBehaviour>().Buffs.Add(((ConsumableSO)Stack.Item).Buff);
        consumer.Behaviour.GetComponent<ParticleBehaviour>().Particles.Add(((ConsumableSO)Stack.Item).Buff.Particles);
        OnCooldown = true;
        Stack.Update(-1);
    }

}