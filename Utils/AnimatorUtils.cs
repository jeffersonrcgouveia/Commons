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
    
        public static int GetNumberMotionsInBlendTree(Animator animator, int layerIndex, int blendTreeHashIndex)
        {
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
            AnimatorStateMachine stateMachine = animatorController.layers[layerIndex].stateMachine;
            foreach (ChildAnimatorState state in stateMachine.states)
            {
                if (state.GetHashCode() == blendTreeHashIndex) 
                {
                    BlendTree _blendTree = state.state.motion as BlendTree;
                    return _blendTree.children.Length;
                }
            }        
            return 0;
        }
    
        public static BlendTree GetBlendTree(Animator animator)
        {
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
        
            AnimatorControllerLayer layer = animatorController.layers[0];
        
            AnimatorStateMachine stateMachine = layer.stateMachine;
            ChildAnimatorStateMachine childAnimatorStateMachine = stateMachine.stateMachines[0];
            AnimatorStateMachine stateMachineChild = childAnimatorStateMachine.stateMachine;
        
            ChildAnimatorState state = stateMachineChild.states[0];
            BlendTree blendTree = state.state.motion as BlendTree;

            return blendTree;
        }
    
        /* AnimatorStateMachine */
    
        public static List<AnimatorStateMachine> FindAnimatorStateMachines(Animator animator, int layerIndex = -1, string stateMachineName = null) 
        {
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

            if (layerIndex >= 0)
            {
                AnimatorControllerLayer layer = animatorController.layers[layerIndex];
                return FindAnimatorStateMachines(layer, stateMachineName);
            }
            else
            {
                List<AnimatorStateMachine> stateMachines = new List<AnimatorStateMachine>();
                foreach (AnimatorControllerLayer layer in animatorController.layers)
                {
                    stateMachines = FindAnimatorStateMachines(layer, stateMachineName);
                }
                return stateMachines; 
            }
        }

        public static List<AnimatorStateMachine> FindAnimatorStateMachines(AnimatorControllerLayer layer, string stateMachineName = null)
        {
            List<AnimatorStateMachine> stateMachines = new List<AnimatorStateMachine>();
        
            AnimatorStateMachine stateMachine = layer.stateMachine;
            if (stateMachineName != null && stateMachineName == stateMachine.name)
            {
                stateMachines.Add(stateMachine);
                return stateMachines;
            }
            foreach (ChildAnimatorStateMachine child in stateMachine.stateMachines)
            {
                stateMachines = FindAnimatorStateMachines(child, stateMachines, stateMachineName);
            }

            return stateMachines;
        }

        private static List<AnimatorStateMachine> FindAnimatorStateMachines(ChildAnimatorStateMachine parent, List<AnimatorStateMachine> result, string stateMachineName = null)
        {
            AnimatorStateMachine stateMachine = parent.stateMachine;
            if (stateMachineName != null)
            {
                if (stateMachineName == stateMachine.name)
                {
                    result.Add(stateMachine);
                }
            }
            else
            {
                result.Add(stateMachine);
            }
            foreach (ChildAnimatorStateMachine child in stateMachine.stateMachines)
            {
                result = FindAnimatorStateMachines(child, result);
            }
        
            return result;
        }
    
    
        /* AnimatorState */
    
        public static List<AnimatorState> FindAnimatorStates(Animator animator, int layerIndex = -1) 
        {
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
    
            if (layerIndex >= 0)
            {
                AnimatorControllerLayer layer = animatorController.layers[layerIndex];
                return FindAnimatorStates(layer);
            }
            else
            {
                List<AnimatorState> result = new List<AnimatorState>();
                foreach (AnimatorControllerLayer layer in animatorController.layers)
                {
                    result = FindAnimatorStates(layer);
                }
                return result; 
            }
        }
    
        public static List<AnimatorState> FindAnimatorStates(AnimatorControllerLayer layer)
        {
            List<AnimatorState> motions = new List<AnimatorState>();
        
            AnimatorStateMachine stateMachine = layer.stateMachine;
            foreach (ChildAnimatorStateMachine child in stateMachine.stateMachines)
            {
                motions = FindAnimatorStates(child, motions);
            }
    
            return motions;
        }
    
        private static List<AnimatorState> FindAnimatorStates(ChildAnimatorStateMachine parent, List<AnimatorState> result)
        {
            foreach (ChildAnimatorState child in parent.stateMachine.states)
            {
                AnimatorState state = child.state;
                if (state)
                {
                    result.Add(state);
                }
            }
            foreach (ChildAnimatorStateMachine child in parent.stateMachine.stateMachines)
            {
                result = FindAnimatorStates(child, result);
            }
        
            return result;
        }
    
        /* Motion */

        public static List<T> FindMotions<T>(Animator animator, int layerIndex = -1) where T : Motion
        {
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

            if (layerIndex >= 0)
            {
                AnimatorControllerLayer layer = animatorController.layers[layerIndex];
                return FindMotions<T>(layer);
            }
            else
            {
                List<T> result = new List<T>();
                foreach (AnimatorControllerLayer layer in animatorController.layers)
                {
                    result = FindMotions<T>(layer);
                }
                return result; 
            }
        }

        public static List<T> FindMotions<T>(AnimatorControllerLayer layer) where T : Motion
        {
            List<T> motions = new List<T>();
        
            AnimatorStateMachine stateMachine = layer.stateMachine;
            foreach (ChildAnimatorStateMachine child in stateMachine.stateMachines)
            {
                motions = FindMotions(child, motions);
            }

            return motions;
        }

        private static List<T> FindMotions<T>(ChildAnimatorStateMachine parent, List<T> result) where T : Motion
        {
            foreach (ChildAnimatorState state in parent.stateMachine.states)
            {
                T motion = state.state?.motion as T;
                if (motion)
                {
                    result.Add(motion);
                }
                BlendTree blendTree = state.state?.motion as BlendTree;
                if (blendTree)
                {
                    foreach (ChildMotion child in blendTree.children)
                    {
                        motion = child.motion as T;
                        if (motion)
                        {
                            result.Add(motion);
                        }
                    }
                }
            }
            foreach (ChildAnimatorStateMachine child in parent.stateMachine.stateMachines)
            {
                result = FindMotions(child, result);
            }
        
            return result;
        }
    }
}