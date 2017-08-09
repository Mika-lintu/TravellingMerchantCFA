using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/TurnAction")]
public class TurnAction : AIAction
{

    public override void Act(StateController controller)
    {

        if (controller.TurnTimerHasElapsed())
        {
            if (controller.TargetInRange())
            {
                controller.Attack();
            }
            else
            {
                controller.SetMoveToTargetState();
            }
        }

    }
}
