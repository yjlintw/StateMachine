using System;
using UnityEngine;

namespace YJL.Fsm
{
    public abstract class Decision : ScriptableObject
    {
        public string nameField = "Decision";
        public abstract void Enter(StateMachine stateMachine);
        public abstract bool Decide(StateMachine stateMachine);
        public abstract void Exit(StateMachine stateMachine);

        public void OnValidate()
        {
            this.name = nameField;
        }
    }
}