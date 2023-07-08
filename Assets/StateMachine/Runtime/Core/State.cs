using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YJL.EditorHelper;
using YJL.Fsm.Helper;

namespace YJL.Fsm
{
    [CreateAssetMenu(menuName = "FSM/State")]
    public class State : BaseState
    {
        public UnityEvent<StateMachine> OnEnter;

        public UnityEvent<StateMachine> OnTick;

        public UnityEvent<StateMachine> OnLateTick;

        public UnityEvent<StateMachine> OnExit;

        [Derived]
        public List<FSMAction> ActionTemplates = new List<FSMAction>();
        public List<Transition> Transitions = new List<Transition>();
        private List<FSMAction> Actions = new List<FSMAction>();  

        public override void Enter(StateMachine stateMachine)
        {
            Actions = new List<FSMAction>(ActionTemplates.Count);
            foreach (FSMAction actionItem in ActionTemplates)
            {
                FSMAction newAction = actionItem.Clone();
                Actions.Add(newAction);
            }
            
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
            
            Actions.Clear();
        }
    }
}