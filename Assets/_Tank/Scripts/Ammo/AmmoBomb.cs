using _Tank.Scripts;
using _Tank.Scripts.Animations;
using UnityEngine;
using Zenject;

public class AmmoBomb : AmmoBase
{
    #region attributes

    private readonly int m_ShowParemeter = Animator.StringToHash("Show");
    private readonly int m_ResetParemeter = Animator.StringToHash("Reset");
    
    [SerializeField] private Animator m_Animator;

    private StateBehaviour m_ExplodeState;

    #endregion

    #region public methods

    public override void Fire(Transform _Transform)
    {
        m_Destroying = false;
        gameObject.transform.SetPositionAndRotation(_Transform.position, _Transform.rotation);
        m_Rigidbody.simulated = true;
        gameObject.SetActive(true);
        m_Animator.SetTrigger(m_ResetParemeter);
        m_Rigidbody.velocity = _Transform.right * m_Speed;
    }

    #endregion

    #region service methods

    protected override void DestroySelf()
    {
        m_Destroying = true;
        
        m_ExplodeState = StateBehaviour.FindState("explode", m_Animator);
        
        if (m_ExplodeState != null)
            m_ExplodeState.OnComplete += OnExplodeFinished;
        
        m_Rigidbody.simulated = false;
        m_Animator.SetTrigger(m_ShowParemeter);
    }

    private void OnExplodeFinished()
    {
        if (m_ExplodeState != null)
            m_ExplodeState.OnComplete -= OnExplodeFinished;
        
        m_Animator.SetTrigger(m_ResetParemeter);

        gameObject.SetActive(false);
        m_Factory.ReturnToPool(this);
        m_Destroying = false;
    }
    
    #endregion
}
