using System;
using System.Collections;
using System.Collections.Generic;
using _Tank.Scripts;
using _Tank.Scripts.Animations;
using UnityEngine;

public class TankHealth : Health
{
    #region attributes

    private readonly int m_ShowParemeter = Animator.StringToHash("Show");
    private readonly int m_ResetParemeter = Animator.StringToHash("Reset");
    
    [SerializeField] private Animator m_Animator;
    
    private StateBehaviour m_ExplodeState;

    #endregion

    #region engine methods

    protected override void OnEnable()
    {
        base.OnEnable();

        m_Animator.SetTrigger(m_ResetParemeter);
        
        Killed += OnKilled;
    }

    protected void OnDisable()
    {
        Killed -= OnKilled;
    }

    #endregion

    #region service methods

    private void OnKilled()
    {
        m_ExplodeState = StateBehaviour.FindState("explode", m_Animator);
        if (m_ExplodeState != null)
            m_ExplodeState.OnComplete += OnExplodeFinished;
        
        m_Animator.SetTrigger(m_ShowParemeter);
    }

    private void OnExplodeFinished()
    {
        if (m_ExplodeState != null)
            m_ExplodeState.OnComplete -= OnExplodeFinished;
        
        gameObject.SetActive(false);
    }

    #endregion
}
