using System;

namespace HungryDogs.Logic.Entities.Persistence
{
    internal class OpeningHour : VersionEntity, HungryDogs.Contracts.Persistence.IOpeningHour
    {
        public int RestaurantId { get; set; }
        public int Weekday { get; set; }
        public TimeSpan OpenFrom { get; set; }
        public TimeSpan OpenTo { get; set; }
        public string Notes { get; set; }
		// Navigation
		public Restaurant Restaurant { get; set; }

	}
}
