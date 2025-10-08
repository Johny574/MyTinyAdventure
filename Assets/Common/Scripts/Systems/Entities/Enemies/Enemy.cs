


using UnityEngine;

public class Enemy : Entity , IPoolObject<Enemy>
{
    public void Bind(Enemy variant) {
        bool melee = variant.GetComponent<MeleeEnemyStatemachine>().enabled;
        bool ranged = variant.GetComponent<MeleeEnemyStatemachine>().enabled;
        bool magic = variant.GetComponent<MeleeEnemyStatemachine>().enabled;

        GetComponent<MeleeEnemyStatemachine>().enabled = melee;
        GetComponent<MeleeBehaviour>().enabled = melee;

        // GetComponent<RangedEnemyStatemachine>().enabled = melee;
        // GetComponent<MeleeBehaviour>().enabled = melee;

        // GetComponent<MeleeEnemyStatemachine>().enabled = melee;
        // GetComponent<MeleeBehaviour>().enabled = melee;


    }
}