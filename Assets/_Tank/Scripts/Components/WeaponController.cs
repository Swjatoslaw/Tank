using System;
using System.Collections;
using System.Collections.Generic;
using _Tank.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class WeaponController : MonoBehaviour
{
    #region attributes

    [Inject] private InputSource m_InputSource;
    [Inject] private IWeapon m_Weapon;
    
    [SerializeField] private GameObject m_Tower;
    [SerializeField] private float m_RotateSpeed;
    
    private IEnumerator m_RotateRoutine;

    #endregion

    #region engine methods

    private void Start()
    {
        Enable();
    }
    
    private void OnDisable()
    {
        Disable();
    }

    public void Enable()
    {
        m_InputSource.Fire.performed += Fire;
        m_InputSource.RotateTower.started += RotateTower;
        m_InputSource.RotateTower.canceled += StopRotateTower;
        m_InputSource.ChangeWeapon.performed += ChangeWeapon;
    }

    public void Disable()
    {
        m_InputSource.Fire.performed -= Fire;
        m_InputSource.RotateTower.started -= RotateTower;
        m_InputSource.RotateTower.canceled -= StopRotateTower;
        m_InputSource.ChangeWeapon.performed -= ChangeWeapon;
        
        DisableRotateRoutine();
    }
    
    #endregion
    
    #region service methods
    
    private void Fire(InputAction.CallbackContext _Obj)
    {
        m_Weapon.Fire();
    }
    
    private void ChangeWeapon(InputAction.CallbackContext _Obj)
    {
        m_Weapon.ChangeWeapon((int) _Obj.ReadValue<float>());
    }
    
    private void RotateTower(InputAction.CallbackContext _Obj)
    {
        DisableRotateRoutine();

        m_RotateRoutine = RotateRoutine(_Obj.ReadValue<float>());
        StartCoroutine(m_RotateRoutine);
    }
    
    private void StopRotateTower(InputAction.CallbackContext _Obj)
    {
        DisableRotateRoutine();
    }
    
    private void DisableRotateRoutine()
    {
        if (m_RotateRoutine != null)
        {
            StopCoroutine(m_RotateRoutine);
            m_RotateRoutine = null;
        }
    }

    private IEnumerator RotateRoutine(float _Direction)
    {
        while (true)
        {
            yield return null;
            m_Tower.transform.Rotate(Vector3.back, _Direction * m_RotateSpeed * Time.deltaTime);
        }
    }

    #endregion
}