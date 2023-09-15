using System;
using _Tank.Scripts;
using _Tank.Scripts.Interfaces;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
public class Enemy : EnemyBase
{
    #region properties

    private Health m_Health;

    #endregion

    #region EngineMethods

    protected override void Init()
    {
        base.Init();
        m_Health = GetComponent<Health>();
    }
    
    private void OnEnable()
    {
        m_Health.Killed += DestroySelf;
    }
    
    private void OnDisable()
    {
        m_Health.Killed -= DestroySelf;
    }

    #endregion

    #region service methods
    protected override void DestroySelf()
    {
        m_Agent.enabled = false;
        gameObject.SetActive(false);
        m_EnemiesController.EnemyDead(this);
    }

    #endregion
}
