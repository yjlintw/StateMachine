using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YJL.Fsm;

namespace YJL.Fsm.Example
{
    [CreateAssetMenu(menuName = "Example/FSM/TimeDecision")]
    public class TimeDecision : Decision
    {
        public float duration = 1.0f;
        private float timer;
        public override void Enter(StateMachine stateMachine)
        {
            timer = Time.time;
        }

        public override bool Decide(StateMachine stateMachine)
        {
            return Time.time > timer + duration;

        }

        public override void Exit(StateMachine stateMachine)
        {
        }
    }
}
