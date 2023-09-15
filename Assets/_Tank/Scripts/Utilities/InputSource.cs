using UnityEngine;
using UnityEngine.InputSystem;

namespace _Tank.Scripts
{
    public class InputSource
    {
        #region attributes

        private Controls m_Controls;

        private InputAction m_Move;
        private InputAction m_Fire;
        private InputAction m_Rotate;
        private InputAction m_RotateTower;
        private InputAction m_ChangeWeapon;

        #endregion

        #region properties

        public InputAction Move => m_Move;
        public InputAction Fire => m_Fire;
        public InputAction Rotate => m_Rotate;
        public InputAction RotateTower => m_RotateTower;
        public InputAction ChangeWeapon => m_ChangeWeapon;

        #endregion

        #region engine methods

        InputSource()
        {
            m_Controls = new Controls();

            m_Move = m_Controls.Ingame.Move;
            m_Move.Enable();

            m_Fire = m_Controls.Ingame.Fire;
            m_Fire.Enable();

            m_Rotate = m_Controls.Ingame.Rotate;
            m_Rotate.Enable();

            m_RotateTower = m_Controls.Ingame.RotateTower;
            m_RotateTower.Enable();

            m_ChangeWeapon = m_Controls.Ingame.ChangeWeapon;
            m_ChangeWeapon.Enable();
        }

        #endregion
    }
}