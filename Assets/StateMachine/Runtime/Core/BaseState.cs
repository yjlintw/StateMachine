using UnityEngine;

namespace YJL.Fsm
{
    public class BaseState : ScriptableObject
    {
        public virtual void Enter(StateMachine stateMachine) {}
        public virtual void Tick(StateMachine stateMachine) {}
        public virtual void LateTick(StateMachine stateMachine) {}
        public virtual void Exit(StateMachine stateMachine) {}
    }
}