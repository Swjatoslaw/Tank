using _Tank.Scripts;
using _Tank.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    #region properties
    
    public EnemyType Type => m_Type;

    #endregion
    
    #region attributes

    [SerializeField] protected EnemyType m_Type;
    [SerializeField] protected float m_Damage;

    protected EnemiesController m_EnemiesController;
    protected NavMeshAgent m_Agent;
    protected bool m_Destroying;

    #endregion

    #region public methods

    public virtual void Spawn(Transform _Transform, EnemiesController _Controller)
    {
        gameObject.transform.SetPositionAndRotation(_Transform.position, _Transform.rotation);
        gameObject.SetActive(true);
        m_EnemiesController = _Controller;
        Init();
    }

    public void UpdateTarget(Vector3 _Target)
    { 
        m_Agent.SetDestination(_Target);
    }

    public void Stop()
    {
        m_Agent.ResetPath();
        m_Agent.enabled = true;
    }

    #endregion
    
    #region engine methods

    protected virtual void Awake()
    {
        Init();
    }

    #endregion

    #region service methods

    protected virtual void Init()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.ResetPath();
        m_Agent.enabled = true;
        m_Agent.updateUpAxis = false;
        m_Agent.updateRotation = false;
    }
    
    protected abstract void DestroySelf();

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (m_Destroying)
            return;

        if (col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(m_Damage);
            DestroySelf();
        }
    }

    #endregion
}