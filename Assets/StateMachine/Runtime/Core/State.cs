using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YJL.EditorHelper;

namespace YJL.Fsm
{
    [CreateAssetMenu(menuName = "FSM/State")]
    public class State : BaseState
    {
        public UnityEvent<StateMachine> OnEnter;

        public UnityEvent<StateMachine> OnTick;

        public UnityEvent<StateMachine> OnLateTick;

        public UnityEvent<StateMachine> OnExit;

        [Expand]
        public List<FSMAction> Actions = new List<FSMAction>();
        public List<Transition> Transitions = new List<Transition>();

        public override void Enter(StateMachine stateMachine)
        {
            foreach (FSMAction action in Actions)
            {
                action.Enter(stateMachine);
            }
            
            OnEnter?.Invoke(stateMachine);
            
            foreach (Transition transition in Transitions)
            {
                transition.Enter(stateMachine);
            }
        }

        public override void Tick(StateMachine stateMachine)
        {
            foreach (FSMAction action in Actions)
            {
                action.Tick(stateMachine);
            }
            OnTick?.Invoke(stateMachine);

            foreach (Transition transition in Transitions)
            {
                transition.Execute(stateMachine);
            }
        }

        public override void LateTick(StateMachine stateMachine)
        {
            foreach (FSMAction action in Actions)
            {
                action.LateTick(stateMachine);
            }
            OnLateTick?.Invoke(stateMachine);
        }

        public override void Exit(StateMachine stateMachine)
        {
            foreach (FSMAction action in Actions)
            {
                action.Exit(stateMachine);
            }
            OnExit?.Invoke(stateMachine);
            
            foreach (Transition transition in Transitions)
            {
                transition.Exit(stateMachine);
            }
        }
    }
}