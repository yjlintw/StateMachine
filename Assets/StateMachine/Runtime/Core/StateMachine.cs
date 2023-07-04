using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YJL.EditorHelper;
using YJL.Fsm.Helper;

namespace YJL.Fsm
{
    public class StateMachine : MonoBehaviour
    {
        [Header("Type Template")]
        [Expand]
        [SerializeField] private BaseState _initialStateTemplate;
        private BaseState _initialState;
        private Dictionary<Type, Component> _chachedComponents;

        public BaseState CurrentState { get; set; }

        private void Awake()
        {
            _chachedComponents = new Dictionary<Type, Component>();
            _initialState = _initialStateTemplate.Clone();
            CurrentState = _initialState;
        }

        private void OnDestroy()
        {
            _initialState = null;
        }

        private void Start()
        {
            CurrentState.Enter(this);
        }

        void Update()
        {
            CurrentState.Tick(this);
        }

        private void LateUpdate()
        {
            CurrentState.LateTick(this);
        }

        public void ChangeState(BaseState newState)
        {
            if (newState == CurrentState || newState is RemainInState) return;
            
            CurrentState.Exit(this);
            CurrentState = newState;
            CurrentState.Enter(this);
        }

        public new T GetComponent<T>() where T : Component
        {
            if (_chachedComponents.ContainsKey(typeof(T)))
            {
                return _chachedComponents[typeof(T)] as T;
            }

            var component = base.GetComponent<T>();
            if (component != null)
            {
                _chachedComponents.Add(typeof(T), component);
            }

            return component;
        }
    }
}