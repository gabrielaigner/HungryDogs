using System;

namespace HungryDogs.Contracts.Modules.Common
{
    [Flags]
    public enum RestaurantState
    {
        InActive = 0b0001,
        Active = 0b0010,
        Closed = 0b0100,
    }
}
