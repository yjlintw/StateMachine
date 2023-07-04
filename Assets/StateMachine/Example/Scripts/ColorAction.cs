using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJL.Fsm.Example
{
    [CreateAssetMenu(menuName = "Example/FSM/Color")]
    public class ColorAction : FSMAction
    {
        public Color color;
        public override void Enter(StateMachine stateMachine)
        {
            Renderer renderer = stateMachine.GetComponent<Renderer>();
            renderer.material.color = color;
        }

        public override void Tick(StateMachine stateMachine)
        {
        }

        public override void LateTick(StateMachine stateMachine)
        {
        }

        public override void Exit(StateMachine stateMachine)
        {
        }
    }
}