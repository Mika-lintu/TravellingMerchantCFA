using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/FleeingAction")]
public class FleeAction : AIAction {

    public override void Act(StateController controller)
    {
        controller.FleeFromBattle();
    }

}
