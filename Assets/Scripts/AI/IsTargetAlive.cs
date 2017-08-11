using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/isTargetAlive")]
public class IsTargetAlive : Decision
{

    public override bool Decide(StateController controller)
    {
        if (controller.health <= 0f)
        {
            controller.Death();
            return false;
        }
        else
        {
            return true;
        }
    }

}
