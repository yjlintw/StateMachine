using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJL.Fsm.Example
{
    [CreateAssetMenu(menuName = "Example/FSM/Color")]
    public class ColorAction : FSMAction
    {
        public Color color;
        public bool flash = false;
        public float flashInterval = 0.5f;
        private float _timer = 0;
        public override void Enter(StateMachine stateMachine)
        {
            Renderer renderer = stateMachine.GetComponent<Renderer>();
            renderer.material.color = color;
            _timer = 0;
            if (!flash)
            {
                renderer.enabled = true;
            }
        }

        public override void Tick(StateMachine stateMachine)
        {
            if (flash)
            {
                _timer += Time.deltaTime;
                if (_timer > flashInterval)
                {
                    Renderer renderer = stateMachine.GetComponent<Renderer>();
                    renderer.enabled = !renderer.enabled;
                    _timer = 0;
                }
            }
        }

        public override void LateTick(StateMachine stateMachine)
        {
        }

        public override void Exit(StateMachine stateMachine)
        {
        }
    }
}