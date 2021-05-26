using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace TopDownMedieval.Plugins.Commons.Utils
{
    public static class AnimatorUtils
    {
        /*----------------------------------------------------------------------------------------*
         * Static Methods
         *----------------------------------------------------------------------------------------*/

        #region AnimatorControllers
        
	    public static AnimatorController FindAnimatorController(RuntimeAnimatorController runtimeAnimatorController)
	    {
		    if (runtimeAnimatorController is AnimatorOverrideController overrideController)
		    {
			    return (AnimatorController) overrideController.runtimeAnimatorController;
		    }
			return (AnimatorController) runtimeAnimatorController;
	    }

	    #endregion

	    #region Layers

	    public static AnimatorControllerLayer FindLayer(RuntimeAnimatorController runtimeAnimatorController, string layerName)
	    {
		    AnimatorController controller = FindAnimatorController(runtimeAnimatorController);
		    return FindLayer(controller, layerName);
	    }

	    public static AnimatorControllerLayer FindLayer(AnimatorController controller, string layerName)
	    {
		    AnimatorControllerLayer layer = null;
		    foreach (AnimatorControllerLayer layerChild in controller.layers)
		    {
			    if (layerChild.name == layerName)
			    {
				    layer = layerChild;
				    break;
			    }
		    }

		    return layer;
	    }

	    #endregion

	    #region StateMachines

	    public static AnimatorStateMachine FindStateMachine(RuntimeAnimatorController runtimeAnimatorController,
		    string layerName, params string[] stateMachineNames)
	    {
		    AnimatorControllerLayer layer = FindLayer(runtimeAnimatorController, layerName);
		    return FindStateMachine(layer, stateMachineNames);
	    }

	    public static AnimatorStateMachine FindStateMachine(AnimatorControllerLayer layer, params string[] stateMachineNames) => FindStateMachine(layer.stateMachine, stateMachineNames);

	    public static AnimatorStateMachine FindStateMachine(AnimatorStateMachine stateMachine, params string[] stateMachineNames)
	    {
		    if (stateMachineNames is null)
		    {
			    throw new ArgumentNullException("stateMachineNames cannot be null");
		    }

		    ChildAnimatorStateMachine[] children = stateMachine.stateMachines;
		    for (int i = 0; i < stateMachineNames.Length; i++)
		    {
			    foreach (ChildAnimatorStateMachine childStateMachine in children)
			    {
				    if (i < stateMachineNames.Length - 1)
				    {
					    children = childStateMachine.stateMachine.stateMachines;
				    }
				    else if (childStateMachine.stateMachine.name == stateMachineNames[i])
				    {
					    return childStateMachine.stateMachine;
				    }
			    }
		    }

		    return stateMachine;
	    }

	    #endregion

	    #region States

	    public static AnimatorState FindState(RuntimeAnimatorController runtimeAnimatorController,
		    string layerName, string stateMachineName, string stateName)
	    {
		    AnimatorStateMachine stateMachine = FindStateMachine(runtimeAnimatorController, layerName, stateMachineName);
		    return FindState(stateMachine, stateName);
	    }

	    public static AnimatorState FindState(AnimatorStateMachine stateMachine, string stateName)
	    {
		    foreach (ChildAnimatorState childState in stateMachine.states)
		    {
			    if (childState.state.name == stateName)
			    {
				    return childState.state;
			    }
		    }

		    return null;
	    }

	    #endregion

	    #region Transitions

	    public static List<AnimatorStateTransition> FindTransitionsByState(RuntimeAnimatorController runtimeAnimatorController,
		    string layerName, string stateMachineName, string stateName, string destinationStateName)
	    {
		    AnimatorState state = FindState(runtimeAnimatorController, layerName, stateMachineName, stateName);

		    List<AnimatorStateTransition> transitions = null;
		    foreach (AnimatorStateTransition childTransition in state.transitions)
		    {
			    if (childTransition.destinationState.name == destinationStateName)
			    {
				    transitions.Add(childTransition);
			    }
		    }

		    return transitions;
	    }

	    public static List<AnimatorStateTransition> FindTransitionsByStateMachine(
		    RuntimeAnimatorController runtimeAnimatorController, string layerName, string stateMachineName,
		    string stateName, string destinationStateMachineName)
	    {
		    AnimatorState state = FindState(runtimeAnimatorController, layerName, stateMachineName, stateName);

		    List<AnimatorStateTransition> transitions = new List<AnimatorStateTransition>();
		    foreach (AnimatorStateTransition childTransition in state.transitions)
		    {
			    if (childTransition.destinationStateMachine.name == destinationStateMachineName)
			    {
				    transitions.Add(childTransition);
			    }
		    }

		    return transitions;
	    }

	    #endregion

    }
}