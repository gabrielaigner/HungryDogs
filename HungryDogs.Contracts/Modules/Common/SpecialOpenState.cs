using System;

namespace HungryDogs.Contracts.Modules.Common
{
    [Flags]
    public enum SpecialOpenState
    {
        NoDefinition = 0,
        Open = 1,                       // 0b0001
        Closed = 2 * Open,              // 0b0010
        IsBusy = 2 * Closed,            // 0b0100
        ClosedPermanent = 2 * IsBusy,   // 0b1000

        ClosedState = Closed + ClosedPermanent,     // 0b1010
    }
}
