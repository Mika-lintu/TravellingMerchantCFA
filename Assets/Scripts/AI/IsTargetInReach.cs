using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/IsTargetInReach")]
public class IsTargetInReach : Decision {

    public override bool Decide(StateController controller)
    {
        return CheckDistance(controller);
    }

    bool CheckDistance(StateController controller)
    {
        if (controller.DistanceToTarget() <= controller.aiStats.range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
