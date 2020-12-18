using HungryDogs.Contracts.Modules.Common;
using System;

namespace HungryDogs.Logic.Entities.Persistence
{
    internal class SpecialOpeningHour : VersionEntity, HungryDogs.Contracts.Persistence.ISpecialOpeningHour
    {
        public int RestaurantId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Notes { get; set; }
        public SpecialOpenState State { get; set; }
        // Navigation
        public Restaurant Restaurant { get; set; }

        public SpecialOpeningHour()
        {
        }

        public SpecialOpeningHour(int restaurantId, DateTime? @from, DateTime? to, string notes, SpecialOpenState state)
        {
            RestaurantId = restaurantId;
            From = @from;
            To = to;
            Notes = notes;
            State = state;
        }
    }
}
