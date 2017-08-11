using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class AIStats : ScriptableObject {

    public float turnSpeed;
    public float range;
    public float movementSpeed;
    [Header("Turn Actions")]
    [RangeAttribute(0, 100)] public int meleeAttack;
    [RangeAttribute(0, 100)] public int rangedAttack;
    [RangeAttribute(0, 100)] public int steal;
    [RangeAttribute(0, 100)] public int flee;
    [RangeAttribute(0, 100)] public int specialAttack;
    [RangeAttribute(0, 100)] public int healSelf;


    public Dictionary<string, int> GetActionsDictionary()
    {
        Dictionary<string, int> actionsDictionary = new Dictionary<string, int>();

        if (meleeAttack > 0) actionsDictionary.Add("MeleeAttack", meleeAttack);
        if (rangedAttack > 0) actionsDictionary.Add("RangedAttack", rangedAttack);
        if (steal > 0) actionsDictionary.Add("Steal", steal);
        if (flee > 0) actionsDictionary.Add("Flee", flee);
        if (specialAttack > 0) actionsDictionary.Add("SpecialAttack", specialAttack);
        if (healSelf > 0) actionsDictionary.Add("HealSelf", healSelf);


        return actionsDictionary;
    } 


}

