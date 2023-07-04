using UnityEngine;

namespace YJL.Fsm
{
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Enter(StateMachine stateMachine);
        public abstract void Tick(StateMachine stateMachine);
        public abstract void LateTick(StateMachine stateMachine);
        public abstract void Exit(StateMachine stateMachine);
    }
}