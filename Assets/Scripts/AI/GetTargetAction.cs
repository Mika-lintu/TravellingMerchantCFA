using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/GetTargetAction")]
public class GetTargetAction : AIAction {

    public override void Act(StateController controller)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<StateController>().deathEvent.AddListener(controller.TargetIsDead);        
    }

}
