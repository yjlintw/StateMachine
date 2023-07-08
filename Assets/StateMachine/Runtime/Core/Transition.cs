using System;
using UnityEditor;
using UnityEngine;
using YJL.EditorHelper;
using YJL.Fsm.Helper;

namespace YJL.Fsm
{
    [Serializable]
    public sealed class Transition
    {
        [Derived]
        public Decision DecisionTemplate;
        public BaseState TrueStateTemplate;
        public BaseState FalseStateTemplate;

        private Decision _decision;
        private BaseState _trueState;
        private BaseState _falseState;

        public void Enter(StateMachine stateMachine)
        {
            _decision = DecisionTemplate.Clone();
            _trueState = TrueStateTemplate.Clone();
            _falseState = FalseStateTemplate.Clone();
            _decision.Enter(stateMachine);
        }


        public void Execute(StateMachine stateMachine)
        {
            if (_decision == null) return;
            
            if (_decision.Decide(stateMachine))
            {
                stateMachine.ChangeState(_trueState);
            }
            else
            {
                stateMachine.ChangeState(_falseState);
            }
        }

        public void Exit(StateMachine stateMachine)
        {
            _decision.Exit(stateMachine);
            _decision = null;
            _trueState = null;
            _falseState = null;
        }
    }
}