using System;

namespace _Tank.Scripts.Interfaces
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage);
        public event Action Killed;
    }
}