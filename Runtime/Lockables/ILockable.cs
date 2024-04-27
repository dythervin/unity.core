using System;

namespace Dythervin
{
    public interface ILockable
    {
        event Action OnLockChanged;

        bool IsLocked { get; }
    }
}