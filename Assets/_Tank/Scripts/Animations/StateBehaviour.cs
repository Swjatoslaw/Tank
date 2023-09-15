using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Tank.Scripts.Animations
{
    public class StateBehaviour : StateMachineBehaviour
    {
        #region attributes

        public string ID;
        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnComplete;

        bool m_Complete;

        #endregion

        #region engine methods

        public override void OnStateEnter(Animator _Animator, AnimatorStateInfo _StateInfo, int _LayerIndex)
        {
            base.OnStateEnter(_Animator, _StateInfo, _LayerIndex);

            m_Complete = false;

            if (OnEnter != null)
                OnEnter();
        }

        public override void OnStateUpdate(Animator _Animator, AnimatorStateInfo _StateInfo, int _LayerIndex)
        {
            base.OnStateUpdate(_Animator, _StateInfo, _LayerIndex);

            if (!m_Complete && OnComplete != null && _StateInfo.normalizedTime >= 1)
            {
                m_Complete = true;
                OnComplete();
            }
        }

        public override void OnStateExit(Animator _Animator, AnimatorStateInfo _StateInfo, int _LayerIndex)
        {
            base.OnStateExit(_Animator, _StateInfo, _LayerIndex);

            if (!m_Complete && OnComplete != null)
            {
                m_Complete = true;
                OnComplete();
            }

            if (OnExit != null)
                OnExit();
        }

        #endregion

        #region public methods

        public static StateBehaviour FindState(string _ID, Animator _Animator)
        {
            if (_Animator != null)
            {
                StateBehaviour[] states = _Animator.GetBehaviours<StateBehaviour>();
                foreach (StateBehaviour state in states)
                {
                    if (state != null && string.Equals(state.ID, _ID, StringComparison.InvariantCultureIgnoreCase))
                        return state;
                }
#if UNITY_EDITOR
                Debug.LogWarning("[StateBehaviour] There is no " + _ID + " behaviour in " + _Animator.name);
#endif
            }

            return null;
        }

        public static StateBehaviour[] FindStates(string _ID, Animator _Animator)
        {
            List<StateBehaviour> states = new List<StateBehaviour>();
            if (_Animator != null)
            {
                StateBehaviour[] behaviours = _Animator.GetBehaviours<StateBehaviour>();
                foreach (StateBehaviour state in behaviours)
                {
                    if (state != null && string.Equals(state.ID, _ID, StringComparison.InvariantCultureIgnoreCase))
                        states.Add(state);
                }
            }

            return states.ToArray();
        }

        void OnDestroy()
        {
            OnEnter = null;
            OnExit = null;
            OnComplete = null;
        }

        #endregion
    }
}