using System;

namespace HungryDogs.Contracts.Persistence
{
    public interface IOpeningHour : IVersionable
    {
        int RestaurantId { get; set; }
        int Weekday { get; set; }
        TimeSpan OpenFrom { get; set; }
        TimeSpan OpenTo { get; set; }
        string Notes { get; set; }
    }
}
