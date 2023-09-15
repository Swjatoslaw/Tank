using System.Collections;
using System.Collections.Generic;
using _Tank.Scripts;
using UnityEngine;

public class WeaponType : MonoBehaviour
{
    #region properties

    public AmmoBase Ammo => m_Ammo;

    public GameObject WeaponPoint => m_WeaponPoint;

    #endregion
    
    #region attributes

    [SerializeField] private AmmoBase m_Ammo;
    [SerializeField] private GameObject m_WeaponPoint;

    #endregion
}
