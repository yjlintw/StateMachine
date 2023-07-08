using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YJL.Fsm;

namespace YJL.Fsm.Example
{
    [CreateAssetMenu(menuName = "Example/FSM/ClickDecision")]
    public class ClickDecision : Decision
    {
        public enum Chirality
        {
            Left,
            Right
        }

        public Chirality chirality = Chirality.Left;
        public override void Enter(StateMachine stateMachine)
        {
        }

        public override bool Decide(StateMachine stateMachine)
        {
            ClickDetect detect = stateMachine.GetComponent<ClickDetect>();
            return chirality == Chirality.Left ? detect.IsLeftClick : detect.IsRightClick;
        }

        public override void Exit(StateMachine stateMachine)
        {
        }
    }
}
