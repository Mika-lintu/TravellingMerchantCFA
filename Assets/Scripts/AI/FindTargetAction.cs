using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FindTargetAction")]
public class FindTargetAction : AIAction {

    public override void Act(StateController controller)
    {
        if (controller.GetNumberOfTargets() > 0)
        {
            controller.FindTarget();
        }
        else
        {
            //something
        }
    }


}
