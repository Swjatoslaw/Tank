using _Tank.Scripts;
using UnityEngine;
using Zenject;

public class AmmoBullet : AmmoBase
{
    #region service methods

    public override void Fire(Transform _Transform)
    {
        gameObject.transform.SetPositionAndRotation(_Transform.position, _Transform.rotation);
        m_Rigidbody.simulated = true;
        gameObject.SetActive(true);
        m_Rigidbody.velocity = _Transform.right * m_Speed;
    }

    protected override void DestroySelf()
    {
        m_Rigidbody.simulated = false;
        gameObject.SetActive(false);
        m_Factory.ReturnToPool(this);
    }

    #endregion
}
