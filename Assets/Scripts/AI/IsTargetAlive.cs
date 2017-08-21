using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/isTargetAlive")]
public class IsTargetAlive : Decision
{

    public override bool Decide(StateController controller)
    {
        if (controller.targetIsAlive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
