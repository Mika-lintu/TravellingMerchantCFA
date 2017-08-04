using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour {

    public float reach;
    public float movementSpeed;
    public float turnSpeed;
    public enum CharacterType { Henchman, Enemy }
    public CharacterType characterType = new CharacterType();
    public List<string> actions;

    public void GetStats(out float return1, out float return2, out float return3, out int return4)
    {
        return1 = reach;
        return2 = movementSpeed;
        return3 = turnSpeed;
        return4 = (int)characterType;
    }

    public List<string> GetActions()
    {
        return actions;
    }
}
