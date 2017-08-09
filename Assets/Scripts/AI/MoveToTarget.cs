using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/MoveToTarget")]
public class MoveToTarget : AIAction {

    public override void Act(StateController controller)
    {
        controller.MoveToTarget();
    }

}
