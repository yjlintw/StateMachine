using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YJL.EditorHelper;

namespace YJL.Fsm
{
    [CreateAssetMenu(menuName = "FSM/StateCollection")]
    public class StateCollection : ScriptableObject
    {
        [Derived]
        public List<State> States;
    }
}