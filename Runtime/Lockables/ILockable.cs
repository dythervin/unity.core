using System;

namespace Dythervin.Core.Lockables
{
    public interface ILockable
    {
        event Action OnLockChanged;

        bool IsLocked { get; }
    }
}