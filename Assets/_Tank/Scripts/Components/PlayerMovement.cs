using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Tank.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region attributes

        private readonly int m_MoveParameter = Animator.StringToHash("Move");
        private readonly WaitForFixedUpdate m_WaitForFixedUpdate = new();
        
        [Inject] private InputSource m_InputSource;

        [SerializeField] private Animator m_Animator;
        [SerializeField] private float m_MoveSpeed;
        [SerializeField] private float m_RotateSpeed;

        private Rigidbody2D m_Rigidbody;
        private IEnumerator m_MoveRoutine;
        private IEnumerator m_RotateRoutine;

        #endregion

        #region engine methods

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Enable();
        }
        
        private void OnDisable()
        {
            Disable();
        }

        #endregion

        #region service methods
        
        private void Enable()
        {
            m_InputSource.Move.started += Move;
            m_InputSource.Move.canceled += StopMove;
            m_InputSource.Rotate.started += Rotate;
            m_InputSource.Rotate.canceled += StopRotate;
        }

        private void Disable()
        {
            m_InputSource.Move.started -= Move;
            m_InputSource.Move.canceled -= StopMove;
            m_InputSource.Rotate.started -= Rotate;
            m_InputSource.Rotate.canceled -= StopRotate;
            
            DisableMoveRoutine();
            DisableRotateRoutine();
        }

        private void Move(InputAction.CallbackContext _Obj)
        {
            DisableMoveRoutine();

            m_MoveRoutine = MoveRoutine(_Obj.ReadValue<float>());
            StartCoroutine(m_MoveRoutine);
            
            UpdateAnimation();
        }
        
        private void Rotate(InputAction.CallbackContext _Obj)
        {
            DisableRotateRoutine();

            m_RotateRoutine = RotateRoutine(_Obj.ReadValue<float>());
            StartCoroutine(m_RotateRoutine);
            
            UpdateAnimation();
        }

        private void StopMove(InputAction.CallbackContext _Obj)
        {
            DisableMoveRoutine();
            UpdateAnimation();
            m_Rigidbody.velocity = Vector2.zero;
        }
        
        private void StopRotate(InputAction.CallbackContext _Obj)
        {
            DisableRotateRoutine();
            UpdateAnimation();
            m_Rigidbody.angularVelocity = 0;
        }

        private void UpdateAnimation()
        {
            m_Animator.SetBool(m_MoveParameter, m_MoveRoutine != null || m_RotateRoutine != null);
        }

        private void DisableMoveRoutine()
        {
            if (m_MoveRoutine != null)
            {
                StopCoroutine(m_MoveRoutine);
                m_MoveRoutine = null;
            }
        }
        
        private void DisableRotateRoutine()
        {
            if (m_RotateRoutine != null)
            {
                StopCoroutine(m_RotateRoutine);
                m_RotateRoutine = null;
            }
        }

        private IEnumerator MoveRoutine(float _Direction)
        {
            while (true)
            {
                yield return m_WaitForFixedUpdate;
                m_Rigidbody.velocity = gameObject.transform.right * m_MoveSpeed * _Direction * Time.deltaTime;
            }
        }
        
        private IEnumerator RotateRoutine(float _Direction)
        {
            while (true)
            {
                yield return m_WaitForFixedUpdate;
                m_Rigidbody.rotation -= _Direction * m_RotateSpeed * Time.deltaTime;
            }
        }
        
        #endregion
    }
}
